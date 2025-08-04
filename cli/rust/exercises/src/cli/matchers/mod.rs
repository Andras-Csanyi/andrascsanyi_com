use std::path::Path;

use self::book::book_matchers;
use self::generate::generate_matchers;
use self::list::list_matchers;
use clap::ArgMatches;

pub mod book;
pub mod generate;
pub mod list;

pub async fn find_matches(arg_matches: ArgMatches, base_path: &Path) {
    generate_matchers(arg_matches.clone(), base_path).await;
    book_matchers(arg_matches.clone(), base_path);
    list_matchers(arg_matches.clone(), base_path);
}
