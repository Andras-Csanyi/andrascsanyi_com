use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::structs::book::Book;

pub fn parse_books(books_path: Vec<PathBuf>) -> Result<Vec<Book>, Error> {
    if books_path.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut books = Vec::new();
        for path in books_path.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<Book>(&r) {
                Ok(mut parsed) => {
                    parsed.set_path(path.into_os_string().into_string().unwrap());
                    books.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}", &r)
                }
            }
        }
        Ok(books)
    }
}
