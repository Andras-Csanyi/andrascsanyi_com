use std::path::Path;

use clap::Error;

use crate::collectors::collector;
use crate::parsers::section;
use crate::structs::chapter::Chapter;
use crate::structs::section::Section;

pub fn execute(chapter: Chapter) -> Result<Vec<Section>, Error> {
    let section_files = collector::collect_recursively(Path::new(chapter.path()), "section.toml")
        .unwrap_or_else(|e| {
            panic!(
                "Could not collect section.toml files under the {} path. Error: {}",
                chapter.path(),
                e
            )
        });
    let parsed_sections = section::parse(section_files)
        .unwrap_or_else(|e| panic!("Cannot parse section files. Error: {}", e));
    Ok(parsed_sections)
}
