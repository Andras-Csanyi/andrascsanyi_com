use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::structs;

pub fn parse(books_path: Vec<PathBuf>) -> Result<Vec<structs::chapter::Chapter>, Error> {
    if books_path.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut chapters = Vec::new();
        for path in books_path.into_iter() {
            let r: String = read_to_string::<_>(path)?;
            match from_str::<structs::chapter::Chapter>(&r) {
                Ok(parsed) => {
                    chapters.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}", &r)
                }
            }
        }
        Ok(chapters)
    }
}
