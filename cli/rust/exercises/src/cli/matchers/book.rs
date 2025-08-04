use std::path::Path;

use clap::ArgMatches;

pub fn book_matchers(arg_matchers: ArgMatches, base_path: &Path) {
    match arg_matchers.subcommand() {
        Some(("book", argument_matches)) => match argument_matches.get_one::<String>("reference") {
            Some(reference) => {
                // logic::get_and_print_details_of_a_book::execute(reference.as_str(), base_path)
            }
            None => {
                println!("No reference value");
            }
        },
        _ => {}
    };
}
