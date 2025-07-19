use std::path::Path;

use clap::Error;

use crate::collectors::books::collect_book_files_absolute_path;
use crate::parsers::books::parse_books;
use crate::structs::book::Book;

pub fn execute(path: &Path) -> Result<Vec<Book>, Error> {
    let book_descriptor_files = collect_book_files_absolute_path(path).unwrap_or_else(|e| {
        panic!(
            "Failed collecting the books files and returned error: {}",
            e
        )
    });
    let books = parse_books(book_descriptor_files)
        .unwrap_or_else(|e| panic!("Failed parsing the books files and returned error: {}", e));
    Ok(books)
}
