use std::path::Path;

use self::matchers::find_matches;

pub mod commands;
pub mod matchers;

// pub async fn exercises_cli(base_path: &Path) {
//     let cli = commands::build_cli();
//     find_matches(cli, base_path).await;
// }
