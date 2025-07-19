use std::path::Path;

use crate::logic::builders;

pub fn execute(base_path: &Path) {
    builders::studies_tree::build(base_path)
        .unwrap_or_else(|e| panic!("Failed to build the studies tree. Error: {}", e));
}
