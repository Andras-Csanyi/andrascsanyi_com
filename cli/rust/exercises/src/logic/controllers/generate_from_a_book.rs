use std::collections::HashMap;
use std::path::Path;

use anyhow::Error;

use crate::logic::builders;
use crate::logic::create_connection;
use crate::logic::sync;

pub async fn execute(base_path: &Path, args: HashMap<String, String>) -> Result<(), Error> {
    let db_connection = create_connection().await?;
    let study_tree = builders::studies_tree::build(base_path)
        .unwrap_or_else(|e| panic!("Failed to build the studies tree. Error: {}", e));
    sync::execute(study_tree.clone(), &db_connection)
        .await
        .unwrap_or_else(|e| panic!("Syncing FS with DB wasn't successful. Error: {}", e));
    // let study_tree_from_db = get_study_tree::get_study_tree(db_connection)
    //     .await
    //     .unwrap_or_else(|e| panic!("failed"));
    Ok(())
}
