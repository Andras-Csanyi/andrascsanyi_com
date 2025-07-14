use std::path::Path;

use clap::ArgMatches;

use self::book::book_matchers;
use self::generate::generate_matchers;
use self::list::list_matchers;

pub mod book;
pub mod generate;
pub mod list;

pub fn find_matches(arg_matches: ArgMatches, base_path: &Path) {
    generate_matchers(arg_matches.clone(), base_path);
    book_matchers(arg_matches.clone(), base_path);
    list_matchers(arg_matches.clone(), base_path);
}
