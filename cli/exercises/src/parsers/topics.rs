use std::fs::read_to_string;
use std::path::PathBuf;

use clap::Error;
use toml::from_str;

use crate::structs::topic::Topic;

pub fn parse_topics(topics_path: Vec<PathBuf>) -> Result<Vec<Topic>, Error> {
    if topics_path.is_empty() {
        panic!("Expected list of topics")
    } else {
        let mut topics = Vec::new();
        for path in topics_path.into_iter() {
            let r: String = read_to_string::<_>(path)?;
            match from_str(&r) {
                Ok(parsed) => {
                    topics.push(parsed);
                }
                Err(e) => {
                    panic!("Cannot parse file: {}", &r)
                }
            }
        }
        Ok(topics)
    }
}
