use std::path::Path;

use tabled::Table;
use tabled::settings::Alignment;
use tabled::settings::Style;

use crate::collectors;
use crate::parsers;

pub fn execute(book_reference: &str, root_path: &Path) {
    println!("the book reference is: {}", book_reference);
    // collect all books
    let book_paths =
        collectors::books::collect_book_files_absolute_path(root_path).unwrap_or_else(|e| {
            panic!(
                "Failed to collect the book paths and returned error: {:#?}",
                e
            )
        });
    let parsed_books = parsers::books::parse_books(book_paths)
        .unwrap_or_else(|e| panic!("Failed parsing the book files and returned error: {:#?}", e));
    // find the book based on the reference
    let target_book = parsed_books
        .iter()
        .find(|b| b.reference() == book_reference)
        .unwrap_or_else(|| panic!("There is no book with the reference {}", book_reference));
    // based on path in the book file collect all chapters of the book
    let chapters =
        collectors::collector::collect_recursively(Path::new(target_book.path()), "chapter.toml")
            .unwrap_or_else(|e| {
                panic!(
                    "Failed to collect {} files under {} directory. Error: {}",
                    "chapter.toml",
                    target_book.path(),
                    e
                )
            });
    let target_book_chapters = parsers::chapter::parse(chapters).unwrap_or_else(|e| {
        panic!(
            "Failed to parse chapter files and returned with error: {:#?}",
            e
        )
    });
    // print it out
    let mut table = Table::new(target_book_chapters);
    table.with(Style::modern());
    table.with(Alignment::center());
    println!("{table}");
}
