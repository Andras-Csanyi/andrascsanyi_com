use std::fs;
use std::path::Path;
use std::path::PathBuf;

use walkdir::Error;
use walkdir::WalkDir;

pub fn collect_topic_files_absolute_path(path: &Path) -> Result<Vec<PathBuf>, Error> {
    let mut topics = Vec::new();
    for entry in WalkDir::new(path)
        .max_depth(2)
        .into_iter()
        .filter_map(|e| e.ok())
    {
        if entry.path().is_file()
            && entry.path().ends_with("topic.toml")
            && entry.path() != Path::new(path)
        {
            match fs::canonicalize(entry.path()) {
                Ok(absolute_path) => topics.push(absolute_path),
                Err(e) => {
                    panic!("Cannot transform path entry to absolute path: {:#?}", e);
                }
            }
        }
        // return Ok(topics);
    }
    Ok(topics)
}
