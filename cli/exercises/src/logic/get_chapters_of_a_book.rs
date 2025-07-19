use std::path::Path;

use clap::Error;

use crate::collectors::collector::collect_recursively;
use crate::parsers::chapter;
use crate::structs::chapter::Chapter;

pub fn execute(book_path: &Path) -> Result<Vec<Chapter>, Error> {
    let chapter_files = collect_recursively(book_path, "chapter.toml")
        .unwrap_or_else(|e| panic!("Failed collecting {} files. Error: {}", "chapter.toml", e));
    let chapters = chapter::parse(chapter_files)
        .unwrap_or_else(|e| panic!("Failed parsing {} files. Error: {}", "chapter.toml", e));
    Ok(chapters)
}
