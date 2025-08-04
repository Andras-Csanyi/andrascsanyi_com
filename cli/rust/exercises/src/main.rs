use std::path::Path;

use self::cli::commands::build_cli;
use self::cli::matchers::find_matches;

pub mod cli;
pub mod logic;

#[tokio::main]
async fn main() {
    let env_map = dotenvy::dotenv()?;
    println!("{:?}", env_map);
    let root_path = "../../docs/book/";
    let path = Path::new(root_path);
    let matches = build_cli();
    find_matches(matches, path).await;
}
