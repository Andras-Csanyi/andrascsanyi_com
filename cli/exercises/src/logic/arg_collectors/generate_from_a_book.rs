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
    match arg_matches.get_one::<usize>("concept") {
        Some(c) => {
            args.insert("concept".to_string(), c.to_string());
        }
        None => (),
    }
    match arg_matches.get_one::<usize>("skill") {
        Some(c) => {
            args.insert("skill".to_string(), c.to_string());
        }
        None => (),
    }
    match arg_matches.get_one::<usize>("application") {
        Some(c) => {
            args.insert("application".to_string(), c.to_string());
        }
        None => (),
    }
    match arg_matches.get_one::<usize>("discussion") {
        Some(c) => {
            args.insert("discussion".to_string(), c.to_string());
        }
        None => (),
    }
    Ok(args)
}
