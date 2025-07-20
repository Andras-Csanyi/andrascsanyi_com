use std::path::Path;

use clap::Error;

use crate::logic::builders;
use crate::logic::controllers::sync_fs_with_db;

pub async fn execute(base_path: &Path) -> Result<(), Error> {
    let study_tree = builders::studies_tree::build(base_path)
        .unwrap_or_else(|e| panic!("Failed to build the studies tree. Error: {}", e));
    sync_fs_with_db::execute(study_tree.clone())
        .await
        .unwrap_or_else(|e| panic!("Syncing FS with DB wasn't successful. Error: {}", e));
    Ok(())
}
