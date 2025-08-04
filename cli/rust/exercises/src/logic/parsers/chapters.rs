use std::collections::HashMap;
use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::logic::structs;

pub fn parse(
    files: HashMap<String, Vec<PathBuf>>,
) -> Result<Vec<structs::chapter::Chapter>, Error> {
    if files.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut chapters = Vec::new();
        let chapter_files_path = files
            .get("chapter")
            .unwrap_or_else(|| panic!("There is no chapter key in the files HashMap"));
        for path in chapter_files_path.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<structs::chapter::Chapter>(&r) {
                Ok(mut parsed) => {
                    parsed.set_path(path.clone().into_os_string().into_string().unwrap());
                    chapters.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}. Error: {}", &r, e)
                }
            }
        }
        Ok(chapters)
    }
}
