use std::path::Path;

use clap::ArgMatches;

pub fn list_matchers(arg_matches: ArgMatches, base_path: &Path) {
    // match arg_matches.subcommand() {
    //     Some(("list", argument_matches)) => {
    //         match argument_matches.subcommand_matches("books") {
    //             Some(_) => match collectors::books::collect_book_files_absolute_path(base_path) {
    //                 Ok(books) => {
    //                     let books: Vec<structs::book::Book> = parsers::books::parse_books(books)
    //                         .unwrap_or_else(|e| panic!("Cannot parse book files. Error: {}", e));
    //                     let mut table = Table::new(books);
    //                     table.with(Alignment::center());
    //                     table.with(Style::modern());
    //                     println!("{table}");
    //                 }
    //                 Err(e) => {
    //                     panic!("Error while parsing book files. Error: {}", e)
    //                 }
    //             },
    //             None => {}
    //         }
    //         match argument_matches.subcommand_matches("topics") {
    //             Some(_) => match collectors::topics::collect_topic_files_absolute_path(base_path) {
    //                 Ok(topics) => {
    //                     let topics: Vec<structs::topic::Topic> =
    //                         parsers::topics::parse_topics(topics).unwrap_or_else(|e| {
    //                             panic!("Cannot parse topic files. Error: {:#?}", e)
    //                         });
    //                     let mut table = Table::new(topics);
    //                     table.with(Alignment::center());
    //                     table.with(Style::modern());
    //                     println!("{table}");
    //                 }
    //                 Err(e) => {
    //                     panic!("Error while parsing topics. Error: {}", e);
    //                 }
    //             },
    //             None => {}
    //         }
    //     }
    //     _ => {}
    // }
}
