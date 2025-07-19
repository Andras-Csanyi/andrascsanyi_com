use std::collections::HashMap;
use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::logic::structs;
use crate::logic::structs::topic::Topic;

pub fn parse(files: HashMap<String, Vec<PathBuf>>) -> Result<Vec<Topic>, Error> {
    if files.is_empty() {
        panic!("Expected list of topics")
    } else {
        let mut topics = Vec::new();
        let topic_files = files
            .get("topic")
            .unwrap_or_else(|| panic!("There is no topic key in the files map."));
        for path in topic_files.into_iter() {
            let r: String = read_to_string::<_>(path.clone())?;
            match from_str::<structs::topic::Topic>(&r) {
                Ok(mut parsed) => {
                    parsed.set_path(path.clone().into_os_string().into_string().unwrap());
                    topics.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}. Error: {}", &r, e)
                }
            }
        }
        Ok(topics)
    }
}
