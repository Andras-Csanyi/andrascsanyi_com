use std::fs;
use std::path::Path;
use std::path::PathBuf;

use walkdir::Error;
use walkdir::WalkDir;

/// Returns a `Vec<PathBuf>` containing all the absolute paths to the books in the system.
///
/// # Returns
/// - `Ok(Vec<PathBuf>)` if the operation was successful.
/// - `Err(Error)` if the operation failed.
pub fn collect_book_files_absolute_path(path: &Path) -> Result<Vec<PathBuf>, Error> {
    let mut books = Vec::new();
    for entry in WalkDir::new(path)
        .max_depth(3)
        .into_iter()
        .filter_map(|e| e.ok())
    {
        if entry.path().is_file()
            && entry.path().ends_with("book.toml")
            && entry.path() != Path::new(path)
        {
            match fs::canonicalize(entry.path()) {
                Ok(absolute_path) => books.push(absolute_path),
                Err(e) => {
                    panic!("Cannot transform path entry to absolute path: {:#?}", e);
                }
            }
        }
        // return Ok(topics);
    }
    Ok(books)
}
