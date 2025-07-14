use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

#[derive(Debug, Serialize, Deserialize, Tabled)]
pub struct Topic {
    #[tabled(rename = "Topic")]
    topic: String,
    #[tabled(rename = "Topic Id")]
    topic_id: String,
}

impl Topic {
    pub fn new(topic: String, topic_id: String) -> Self {
        Self { topic, topic_id }
    }

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
}
