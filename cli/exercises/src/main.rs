use std::path::Path;

use self::cli::commands::build_cli;
use self::cli::matchers::find_matches;

pub mod cli;
pub mod logic;

#[tokio::main]
async fn main() {
    let root_path = "../../docs/book/";
    let path = Path::new(root_path);
    let matches = build_cli();
    find_matches(matches, path).await;

    // let matches = command!()
    //     .propagate_version(true)
    //     .subcommand_required(true)
    //     .arg_required_else_help(true)
    // .subcommand(
    //
    //     Command::new("generate")
    //         .about("Generating exercises from the provided topic, book and chapters.")
    //         .subcommand(
    //
    //             Command::new("book")
    //             .about("
    //                 When the exercise list generation is based on a single book.
    //                 When no volumes are provided for the question types the main volume will be
    //                 equally split among the types.
    //                 ")
    //             .arg(
    //                 arg!(
    //                     -b
    //                     --book <BOOK>
    //                     "The reference of the book")
    //                 .required(true)
    //                 .value_parser(value_parser!(String)),
    //             )
    //             .arg(
    //                 arg!(
    //                     -c
    //                     --chapter <CHAPTER>
    //                     "From which chapter the exercises going to be generated.")
    //                 .required(false)
    //                 .value_parser(value_parser!(PathBuf)),
    //             )
    //             .arg(
    //                 arg!(
    //                     --concept <VOLUME>
    //                     "The volume of concept checking questions to be generated")
    //                 .required(false)
    //                 .value_parser(value_parser!(usize)),
    //             )
    //             .arg(
    //                 arg!(
    //                     --skill <VOLUME>
    //                     "The volume of skill checking questions to be generated"
    //                 )
    //                 .required(false)
    //                 .value_parser(value_parser!(usize)),
    //             )
    //             .arg(
    //                 arg!(
    //                     --applications <VOLUME>
    //                     "The volume of application checking questions to be generated")
    //                 .required(false)
    //                 .value_parser(value_parser!(usize)),
    //             )
    //             .arg(
    //                 arg!(
    //                     --theories <VOLUME>
    //                     "The volume of theory and their proves checking questions to be generated")
    //                 .required(false)
    //                 .value_parser(value_parser!(usize)),
    //             )
    //             .arg(
    //                 arg!(
    //                     -o
    //                     --output <FILE>
    //                     "Output file")
    //                 .required(false)
    //                 .value_parser(value_parser!(String)),
    //             )
    //             .arg(
    //                 arg!(
    //                     --volume <FILE>
    //                     "The main volume of questions."
    //                 )
    //                 .long_help(
    //                     "When any of the type questions have volume provided that will be removed from this volume and the remainder will be provided for the one doesn't have volume provided."
    //                 )
    //                 .required(false)
    //                 .value_parser(value_parser!(String)),
    //             )
    //         )
    //         .subcommand(
    //             Command::new("topic")
    //             .about("
    //                 When the exercise list generation is based on a topic.
    //                 It means that multiple books may be included.
    //                 ")
    //             )
    //         //     .arg(
    //         //         arg!(-t --topic <TOPIC> "The reference of the topic")
    //         //             .required(true)
    //         //             .value_parser(value_parser!(String)),
    //         //         )
    //         //     .arg(
    //         //         arg!(
    //         //             -c
    //         //             --concept_checker_questions <VOLUME>
    //         //             "The volume of concept checking questions to be generated")
    //         //         .required(false)
    //         //         .value_parser(value_parser!(usize)),
    //         //     )
    //         //     .arg(
    //         //         arg!(
    //         //             -s
    //         //             --skill_checker_questions <VOLUME>
    //         //             "The volume of skill checking questions to be generated"
    //         //         )
    //         //         .required(false)
    //         //         .value_parser(value_parser!(usize)),
    //         //     )
    //         //     .arg(
    //         //         arg!(
    //         //             -a
    //         //             --applications_checker_questions <VOLUME>
    //         //             "The volume of application checking questions to be generated")
    //         //         .required(false)
    //         //         .value_parser(value_parser!(usize)),
    //         //     )
    //         //     .arg(
    //         //         arg!(
    //         //             -t
    //         //             --theories_and_prove_checker_questions <VOLUME>
    //         //             "The volume of theory and their proves checking questions to be generated")
    //         //         .required(false)
    //         //         .value_parser(value_parser!(usize)),
    //         //     )
    //         //     .arg(
    //         //         arg!(
    //         //             -o
    //         //             --output <FILE>
    //         //             "Output file")
    //         //         .required(false)
    //         //         .value_parser(value_parser!(String)),
    //         //     )
    // )
    // .subcommand(
    //     Command::new("list")
    //         .about(
    //             "Listing the available books and topics.
    //             It is important to note that TOPICS is a bigger set than BOOKS.
    //             When questions generated by TOPICS the result may have questions across
    //             multiple books.
    //             ",
    //         )
    //         .subcommands([
    //             Command::new("books")
    //                 .about("Listing the available books to generate testing questions."),
    //             Command::new("topics")
    //                 .about("Listing the available topics to generate testing questions."),
    //         ]),
    // )
    // .subcommand(
    //     Command::new("book")
    //         .about(
    //             "Getting the details of a book.
    //             The particular book's reference must be provided and can be obtained by
    //             'exercises list books'.
    //             ",
    //         )
    //         .arg(
    //             arg!(-r --reference <REF> "The reference of the book.")
    //                 .required(true)
    //                 .value_parser(value_parser!(String)),
    //         )
    //         .arg(
    //             arg!(-e --example <EX> "Just an example")
    //                 .required(false)
    //                 .value_parser(value_parser!(String)),
    //         ),
    // )
    // .get_matches();

    // match matches.subcommand() {
    // Some(("generate", generate_subcommand_matches)) => {
    //     match generate_subcommand_matches.subcommand_matches("book") {
    //         Some(book_match) => logic::generate_from_a_book::execute(book_match, path),
    //         None => {}
    //     }
    //     match generate_subcommand_matches.subcommand_matches("topic") {
    //         Some(topic_matches) => {
    //             println!("generate book matches: {:#?}", topic_matches)
    //         }
    //         None => {}
    //     }
    // }
    // Some(("list", argument_matches)) => {
    //     match argument_matches.subcommand_matches("books") {
    //         Some(_) => match collectors::books::collect_book_files_absolute_path(path) {
    //             Ok(books) => {
    //                 let books: Vec<structs::book::Book> = parsers::books::parse_books(books)
    //                     .unwrap_or_else(|e| panic!("Cannot parse book files. Error: {}", e));
    //                 let mut table = Table::new(books);
    //                 table.with(Alignment::center());
    //                 table.with(Style::modern());
    //                 println!("{table}");
    //             }
    //             Err(e) => {
    //                 panic!("Error while parsing book files. Error: {}", e)
    //             }
    //         },
    //         None => {}
    //     }
    //     match argument_matches.subcommand_matches("topics") {
    //         Some(_) => match collectors::topics::collect_topic_files_absolute_path(path) {
    //             Ok(topics) => {
    //                 let topics: Vec<structs::topic::Topic> =
    //                     parsers::topics::parse_topics(topics).unwrap_or_else(|e| {
    //                         panic!("Cannot parse topic files. Error: {:#?}", e)
    //                     });
    //                 let mut table = Table::new(topics);
    //                 table.with(Alignment::center());
    //                 table.with(Style::modern());
    //                 println!("{table}");
    //             }
    //             Err(e) => {
    //                 panic!("Error while parsing topics. Error: {}", e);
    //             }
    //         },
    //         None => {}
    //     }
    // }
    // Some(("book", argument_matches)) => match argument_matches.get_one::<String>("reference") {
    //     Some(reference) => {
    //         logic::get_and_print_details_of_a_book::execute(reference.as_str(), path)
    //     }
    //     None => {
    //         println!("No reference value");
    //     }
    // },
    // _ => unreachable!("Bad things happened."),
}
