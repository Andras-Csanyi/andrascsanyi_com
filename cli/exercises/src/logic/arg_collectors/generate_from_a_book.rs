use std::collections::HashMap;

use clap::ArgMatches;
use clap::Error;

pub fn generate_from_a_book(arg_matches: ArgMatches) -> Result<HashMap<String, String>, Error> {
    let mut args: HashMap<String, String> = HashMap::new();
    args.insert(
        "book".to_string(),
        arg_matches
            .get_one::<String>("book")
            .expect("No book reference was provided.")
            .clone(),
    );
    args.insert(
        "chapters".to_string(),
        arg_matches
            .get_one::<String>("chapters")
            .expect("No chapter references were provided.")
            .clone(),
    );
    // args.insert(
    //     "concept".to_string(),
    //     arg_matches
    //         .get_one::<String>("concept")
    //         .expect("No concept questions volume was provided.")
    //         .clone(),
    // );
    // args.insert(
    //     "skills".to_string(),
    //     arg_matches
    //         .get_one::<String>("skills")
    //         .expect("No skill questions volume was provided.")
    //         .clone(),
    // );
    // args.insert(
    //     "applications".to_string(),
    //     arg_matches
    //         .get_one::<String>("applications")
    //         .expect("No applications questions volume was provided.")
    //         .clone(),
    // );
    // args.insert(
    //     "theories".to_string(),
    //     arg_matches
    //         .get_one::<String>("theories")
    //         .expect("No theories questions volume was provided.")
    //         .clone(),
    // );
    Ok(args)
}
