use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::structs;
use crate::structs::section::Section;

pub fn parse(path: Vec<PathBuf>) -> Result<Vec<Section>, Error> {
    if path.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut sections = Vec::new();
        for path in path.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<structs::section::Section>(&r) {
                Ok(parsed) => {
                    sections.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}, Error: {}", &r, e)
                }
            }
        }
        Ok(sections)
    }
}
