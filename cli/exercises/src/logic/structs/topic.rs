use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

use super::book::Book;

#[derive(Clone, Debug, Serialize, Deserialize, Tabled)]
pub struct Topic {
    #[tabled(rename = "Topic name")]
    topic_name: String,

    #[tabled(rename = "Topic CLI Reference")]
    topic_cli_reference: String,

    #[tabled(rename = "Path")]
    #[serde(skip)]
    path: String,

    #[tabled(skip)]
    #[serde(skip)]
    books: Vec<Book>,
}

impl Topic {
    pub fn topic_name(&self) -> &str {
        &self.topic_name
    }

    pub fn set_topic_name(&mut self, topic_name: String) {
        self.topic_name = topic_name;
    }

    pub fn topic_cli_reference(&self) -> &str {
        &self.topic_cli_reference
    }

    pub fn set_topic_cli_reference(&mut self, topic_cli_reference: String) {
        self.topic_cli_reference = topic_cli_reference;
    }

    pub fn set_path(&mut self, path: String) {
        self.path = path;
    }

    pub fn path(&self) -> &str {
        &self.path
    }

    pub fn books_mut(&mut self) -> &mut Vec<Book> {
        &mut self.books
    }

    pub fn books(&self) -> &[Book] {
        &self.books
    }
}
