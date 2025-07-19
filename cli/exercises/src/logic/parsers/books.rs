use std::collections::HashMap;
use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::logic::structs::book::Book;

pub fn parse(files: HashMap<String, Vec<PathBuf>>) -> Result<Vec<Book>, Error> {
    if files.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut books = Vec::new();
        let book_file_paths = files
            .get("book")
            .unwrap_or_else(|| panic!("There is no book key in the files HashMap"));

        for path in book_file_paths.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<Book>(&r) {
                Ok(mut parsed) => {
                    parsed.set_path(path.clone().into_os_string().into_string().unwrap());
                    books.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}. Error: {}", &r, e)
                }
            }
        }
        Ok(books)
    }
}
