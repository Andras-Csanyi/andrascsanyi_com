use std::fs;
use std::path::Path;
use std::path::PathBuf;

use clap::Error;
use walkdir::WalkDir;

pub fn collect_recursively(path: &Path, file_name: &str) -> Result<Vec<PathBuf>, Error> {
    let mut files = Vec::new();
    for entry in WalkDir::new(path.parent().unwrap())
        .into_iter()
        .filter_map(|e| e.ok())
    {
        if entry.path().is_file()
            && entry.path().ends_with(file_name)
            && entry.path() != Path::new(path)
        {
            match fs::canonicalize(entry.path()) {
                Ok(absolute_path) => files.push(absolute_path),
                Err(e) => {
                    panic!("Cannot transform path entry to absolute path: {:#?}", e);
                }
            }
        }
        // return Ok(topics);
    }
    Ok(files)
}
