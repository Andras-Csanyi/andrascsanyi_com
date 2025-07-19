use clap::Command;
use clap::arg;
use clap::value_parser;

pub fn book_subcommand(commands: &mut Command) -> Command {
    commands.clone().subcommand(
        Command::new("book")
            .about(
                "Getting the details of a book.
                    The particular book's reference must be provided and can be obtained by
                    'exercises list books'.
                    ",
            )
            .arg(
                arg!(-r --reference <REF> "The reference of the book.")
                    .required(true)
                    .value_parser(value_parser!(String)),
            )
            .arg(
                arg!(-e --example <EX> "Just an example")
                    .required(false)
                    .value_parser(value_parser!(String)),
            ),
    )
}
