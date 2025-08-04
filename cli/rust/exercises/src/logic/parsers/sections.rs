use std::collections::HashMap;
use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::logic::structs::section::Section;

pub fn parse(files: HashMap<String, Vec<PathBuf>>) -> Result<Vec<Section>, Error> {
    if files.is_empty() {
        panic!("Expected list of books")
    } else {
        let mut sections = Vec::new();
        let section_files = files
            .get("section")
            .unwrap_or_else(|| panic!("There is no section key in the files map."));
        for files in section_files.into_iter() {
            let r: String = read_to_string::<_>(files.clone())?;
            match from_str::<Section>(&r) {
                Ok(mut parsed) => {
                    parsed.path = files.clone().into_os_string().into_string().unwrap();
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
