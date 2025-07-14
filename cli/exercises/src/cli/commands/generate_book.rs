use std::path::PathBuf;

use clap::arg;
use clap::value_parser;
use clap::Command;

pub fn generate_book_command(commands: Command) -> Command {
    commands
        .clone()
        .subcommand(
            Command::new("generate")
                .about("Generating exercises from the provided topic, book and chapters.")
                .subcommand(

                    Command::new("book")
                    .about("
                        When the exercise list generation is based on a single book.
                        When no volumes are provided for the question types the main volume will be
                        equally split among the types.
                        ")
                    .arg(
                        arg!(
                            --book <BOOK> 
                            "The reference of the book")
                        .required(true)
                        .value_parser(value_parser!(String)),
                    )
                    .arg(
                        arg!(
                            --chapters <CHAPTERS> 
                            "From which chapters the exercises going to be generated.")
                        .required(false)
                        .value_parser(value_parser!(String)),
                    )
                    .arg(
                        arg!(
                            --concept <VOLUME> 
                            "The volume of concept checking questions to be generated")
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            --skill <VOLUME> 
                            "The volume of skill checking questions to be generated"
                        )
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            --applications <VOLUME> 
                            "The volume of application checking questions to be generated")
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            --theories <VOLUME> 
                            "The volume of theory and their proves checking questions to be generated")
                        .required(false)
                        .value_parser(value_parser!(usize)),
                    )
                    .arg(
                        arg!(
                            -o 
                            --output <FILE> 
                            "Output file")
                        .required(false)
                        .value_parser(value_parser!(String)),
                    )
                    .arg(
                        arg!(
                            --volume <FILE> 
                            "The main volume of questions."
                        )
                        .long_help(
                            "When any of the type questions have volume provided that will be removed from this volume and the remainder will be provided for the one doesn't have volume provided."
                        )
                        .required(false)
                        .value_parser(value_parser!(String)),
                    )
                )
        )
}
