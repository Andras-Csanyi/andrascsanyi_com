import os
import random
import copy

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
    collection.models.update_dict(gnc_note)
    assert gnc_note is not None, "Note is None after name change"

    # collection.models.add_field(
    #     gnc_note,
    #     {
    #         "name": "Examples",
    #         "ord": 2,
    #         "sticky": False,
    #         "rtl": False,
    #         "font": "Arial",
    #         "size": 20,
    #         "description": "",
    #         "plainText": False,
    #         "collapsed": False,
    #         "excludeFromSearch": False,
    #         "tag": None,
    #         "preventDeletion": False,
    #     },
    # )
    # print("right after update", gnc_note)
    # changes = collection.models.update_dict(gnc_note)
    # print(changes)

    latex_pre = """
    \\documentclass[12pt]{article}
    \\special{papersize=3in,5in}
    \\usepackage[utf8]{inputenc}
    \\usepackage{amssymb}
    \\usepackage{amsmath}
    \\usepackage{booktabs}
    \\usepackage{makecell}
    \\usepackage{tabularx}
    \\usepackage[english]{babel}
    \\pagestyle{empty}
    \\setlength{\\parindent}{0in}
    \\begin{document}\n
    """
    gnc_note["latexPre"] = latex_pre
    collection.models.update_dict(gnc_note)
    gnc_note = collection.models.by_name("mathematics-generated")
    assert gnc_note is not None, "Note is None after latex pre update"

    # latex_pre = """
    #             \\documentclass[12pt]{article}
    #             \\special{papersize=3in,5in}
    #             \\usepackage[utf8]{inputenc}
    #             \\usepackage{amssymb}
    #             \\usepackage{amsmath}
    #             \\usepackage{booktabs}
    #             \\usepackage{makecell}
    #             \\usepackage{tabularx}
    #             \\usepackage[english]{babel}
    #             \\pagestyle{empty}
    #             \\setlength{\\parindent}{0in}
    #             \\begin{document}\n
    # """
    # basic_model["latexPre"] = latex_pre
    # collection.models.update(basic_model)
    # basic_model_updated = collection.models.by_name("Basic")
    # assert basic_model_updated is not None, "Basic model is None"

    # mathematics_model_orig = collection.models.by_name("Mathematics - Generated")
    # mathematics_model_automatic: NotetypeDict = copy.deepcopy(basic_model)
    # mathematics_model_automatic["id"] = random.randint(1, 9999999999999)  # noqa: F821
    # mathematics_model_automatic["tmpls"][0]["id"] = random.randint(1, 9999999999999)
    #
    # if mathematics_model_orig is None:
    #     # the Mathematics card type is based on the Basic
    #     # ord 0 is Front
    #     # ord 1 is Back
    #
    #     front = None
    #     if "flds" in mathematics_model_automatic:
    #         front = next(
    #             (
    #                 fld
    #                 for fld in mathematics_model_automatic["flds"]
    #                 if fld.get("name") == "Front"
    #             ),
    #             None,
    #         )
    #         assert front is not None, "Fron is None"
    #         example = copy.deepcopy(front)
    #
    #         mathematics_model_automatic["flds"][0]["id"] = random.randint(
    #             1, 9999999999999
    #         )
    #         mathematics_model_automatic["flds"][1]["id"] = random.randint(
    #             1, 9999999999999
    #         )
    #
    #         assert example is not None, "Example is None"
    #         example["name"] = "Example"
    #         example["ord"] = 2
    #         example["id"] = random.randint(1, 9999999999999)
    #         mathematics_model_automatic["flds"].append(example)
    #
    #         theorem = copy.deepcopy(front)
    #         assert theorem is not None, "Theorem is None"
    #         theorem["name"] = "Theorem"
    #         theorem["ord"] = 3
    #         theorem["id"] = random.randint(1, 9999999999999)
    #         mathematics_model_automatic["flds"].append(theorem)
    #
    #         proof = copy.deepcopy(front)
    #         assert proof is not None, "Proof is None"
    #         proof["name"] = "Proof"
    #         proof["ord"] = 4
    #         proof["id"] = random.randint(1, 9999999999999)
    #         mathematics_model_automatic["flds"].append(proof)
    #
    #         source_book = copy.deepcopy(front)
    #         assert source_book is not None, "Source book is None"
    #         source_book["name"] = "Source Book"
    #         source_book["ord"] = 5
    #         source_book["id"] = random.randint(1, 9999999999999)
    #         mathematics_model_automatic["flds"].append(source_book)
    #
    #         source_book_page_reference = copy.deepcopy(front)
    #         assert source_book_page_reference is not None, (
    #             "Source book page reference is None"
    #         )
    #         source_book_page_reference["name"] = "Page reference"
    #         source_book_page_reference["ord"] = 6
    #         source_book_page_reference["id"] = random.randint(1, 9999999999999)
    #         mathematics_model_automatic["flds"].append(source_book_page_reference)
    #
    #         latex_pre = """
    #             \\documentclass[12pt]{article}
    #             \\special{papersize=3in,5in}
    #             \\usepackage[utf8]{inputenc}
    #             \\usepackage{amssymb}
    #             \\usepackage{amsmath}
    #             \\usepackage{booktabs}
    #             \\usepackage{makecell}
    #             \\usepackage{tabularx}
    #             \\usepackage[english]{babel}
    #             \\pagestyle{empty}
    #             \\setlength{\\parindent}{0in}
    #             \\begin{document}\n
    #         """
    #         mathematics_model_automatic["latexPre"] = latex_pre
    #
    #         latex_post = "\\end{document}"
    #         mathematics_model_automatic["latexPost"] = latex_post
    #
    # assert mathematics_model_automatic is not None, "Mathematics model is None"
    # # adding a new card
    #
    # print(mathematics_model_automatic)
    # # print(collection.models.all())
    # collection.models.add_dict(mathematics_model_automatic)
    # math_model = collection.models.by_name("Mathematics - Generated")
    # assert math_model is not None, "Math model is None"

    new_card = collection.new_note(gnc_note)

    new_card["Front"] = "test question"
    new_card["Back"] = """
        [latex]
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
        $ \\frac{a}{b} $
            \\begin{tabularx}{1\\textwidth}{
                p{\\dimexpr0.5\\textwidth\\relax}
                p{\\dimexpr0.5\\textwidth\\relax}
            }
            \\toprule
            \\midrule
            \\end{tabularx}
        [/latex]"""

    # new_card["Example"] = "[latex]$ \\frac{a}{b}$ [/latex]"
    # new_card["Theorem"] = "[latex]$ \\frac{a}{b}$ [/latex]"
    # new_card["Proof"] = "[latex]$ \\frac{a}{b}$ [/latex]"
    # new_card["Source Book"] = "James Stewart: Calculus"
    # new_card["Page reference"] = "10"

    collection.add_note(note=new_card, deck_id=deck_id)

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
