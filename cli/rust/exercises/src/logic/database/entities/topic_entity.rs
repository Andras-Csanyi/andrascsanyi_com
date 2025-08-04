use sqlx::prelude::FromRow;

#[derive(FromRow, Debug)]
pub struct TopicEntity {
    pub id: i32,
    #[sqlx(default)]
    pub topic_name: Option<String>,
    #[sqlx(default)]
    pub topic_cli_reference: Option<String>,
}

impl TopicEntity {
    pub fn new(id: i32, topic_name: String, topic_cli_reference: String) -> Self {
        Self {
            id,
            topic_name,
            topic_cli_reference,
        }
    }
}
