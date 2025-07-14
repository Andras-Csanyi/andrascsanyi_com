use std::collections::HashMap;
use std::path::Path;

use crate::logic::get_chapters_of_a_book;

use super::get_list_of_books;

pub fn execute(args: HashMap<String, String>, path: &Path) {
    let book_reference = args
        .get("book")
        .unwrap_or_else(|| panic!("No book reference was provided."));
    let books = get_list_of_books::execute(path)
        .unwrap_or_else(|e| panic!("Failed parsing the books and returned error: {}", e));
    let target_book = books
        .into_iter()
        .find(|b| b.reference() == book_reference)
        .unwrap_or_else(|| panic!("There is no book with reference: {}", book_reference));
    println!("{:#?}", target_book);
    let chapters =
        get_chapters_of_a_book::execute(Path::new(target_book.path())).unwrap_or_else(|e| {
            panic!(
                "Error happened while collecting chapters of {} book. Error: {}",
                target_book.title(),
                e
            )
        });
    println!("{:#?}", chapters);
}
