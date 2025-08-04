use clap::ArgMatches;
use clap::command;

use self::generate_book::generate_book_command;
use self::list::list_subcommand;

pub mod book_subcommand;
pub mod generate_book;
pub mod list;

pub fn build_cli() -> ArgMatches {
    let commands = command!()
        .propagate_version(true)
        .arg_required_else_help(true);
    let generate_book_subcommand_added = generate_book_command(commands);
    let list_subcommand_added = list_subcommand(generate_book_subcommand_added);
    list_subcommand_added.get_matches()
}
