import os
import re
import random
import copy

from pathlib import Path
from typing import List
from pprint import pprint
from anki.collection import Collection, DeckIdLimit
from anki.decks import DeckId
from anki.import_export_pb2 import ExportAnkiPackageOptions
from anki.models import FieldDict, NotetypeDict


def create_deck():
    currdir = os.path.dirname(os.path.abspath(__file__))
    collection_path = os.path.join(currdir, "collection_workdir.anki2")

    collection = Collection(collection_path)
    deck_id = collection.decks.id("Applied Math and GNC", create=True)
    assert deck_id is not None, "Deck is None"

    basic_model = collection.models.by_name("Basic")
    assert basic_model is not None, "Basic model is None"

    gnc_note = collection.models.copy(basic_model)
    assert gnc_note is not None, "Note is None"

    gnc_note["name"] = "mathematics-generated"
    # enables svg generation, needed for tikz
    gnc_note["latexsvg"] = True
    collection.models.update_dict(gnc_note)
    assert gnc_note is not None, "Note is None after name change"

    latex_pre = """\\documentclass[dvisvgm]{standalone}
    \\special{papersize=3in,5in}
    \\usepackage[utf8]{inputenc}
    \\usepackage{amssymb}
    \\usepackage{amsmath}
    \\usepackage{booktabs}
    \\usepackage{makecell}
    \\usepackage{tabularx}
    \\usepackage{tikz}
    \\usepackage[english]{babel}
    \\pagestyle{empty}
    \\setlength{\\parindent}{0in}
    \\begin{document}"""

    gnc_note["latexPre"] = latex_pre
    collection.models.update_dict(gnc_note)
    gnc_note = collection.models.by_name("mathematics-generated")
    assert gnc_note is not None, "Note is None after latex pre update"

    print(currdir)
    latex_root_path = Path("./docs/book")
    ankicard_flag = "%ankicard"
    back_content_pattern = (
        r"\\subsection(?:\[[^\]]*\])?{([^}]*)}([\s\S]*?)%ankicard end"
    )
    tags_pattern = r"^%ankitags(?:\s+([^\n]*))?$"

    try:
        for tex_file in latex_root_path.rglob("*.tex"):
            if tex_file.is_file():
                print(f"processed file: {tex_file}")
                content = tex_file.read_text(encoding="utf-8")
                if ankicard_flag not in content:
                    print(f"{tex_file} is skipped.")
                    continue

                back_content = re.findall(back_content_pattern, content)

                if len(back_content) != 0:
                    for front, back in back_content:
                        new_card = collection.new_note(gnc_note)
                        new_card["Front"] = front.replace("\\\\", "\\")
                        new_card["Back"] = "[latex]" + back + "[/latex]"
                        tag_matches = re.findall(tags_pattern, content, re.MULTILINE)
                        tags: List[str] = []
                        for tag_match in tag_matches:
                            if not tag_match:
                                print(
                                    f"skipped adding tags for {tex_file} because no tags found"
                                )
                            else:
                                tags = [
                                    tag.strip()
                                    for tag in tag_match.split()
                                    if tag.strip()
                                ]
                                for t in tags:
                                    new_card.add_tag(t)
                                print(f"Tags: {tags}")

                        # adding to the collection
                        collection.add_note(note=new_card, deck_id=deck_id)

                print("processing finished \n")

    except Exception as e:
        print(f" Error happened: {e}")

    # new_card.add_tag("Mathematics")
    # new_card.add_tag("Calculus")
    # new_card.add_tag("Custom")

    export_options = ExportAnkiPackageOptions()
    export_options.with_deck_configs = True
    export_options.with_media = True
    export_limit = DeckIdLimit(deck_id)
    export_name = currdir + "/whatever.apkg"
    collection.export_anki_package(
        out_path=export_name, options=export_options, limit=export_limit
    )

    collection.close()


create_deck()
