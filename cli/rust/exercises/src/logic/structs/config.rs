use serde::Deserialize;

#[derive(Debug, Deserialize)]
pub struct Config {
    database: Database,
}

impl Config {
    pub fn database(&self) -> &Database {
        &self.database
    }
}

#[derive(Debug, Deserialize)]
pub struct Database {
    pub url: String,
}

impl Database {
    pub fn url(&self) -> &str {
        &self.url
    }
}
