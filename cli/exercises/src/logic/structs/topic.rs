use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

use super::book::Book;

#[derive(Clone, Debug, Serialize, Deserialize, Tabled)]
pub struct Topic {
    #[tabled(rename = "Topic")]
    topic: String,

    #[tabled(rename = "Topic Id")]
    topic_id: String,

    #[tabled(rename = "Path")]
    #[serde(skip)]
    path: String,

    #[tabled(skip)]
    #[serde(skip)]
    books: Vec<Book>,
}

impl Topic {
    pub fn topic(&self) -> &str {
        &self.topic
    }

    pub fn set_topic(&mut self, topic: String) {
        self.topic = topic;
    }

    pub fn topic_id(&self) -> &str {
        &self.topic_id
    }

    pub fn set_topic_id(&mut self, topic_id: String) {
        self.topic_id = topic_id;
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
}
