use sqlx::prelude::FromRow;

#[derive(FromRow, Debug)]
pub struct TopicEntity {
    pub id: i32,
    #[sqlx(default)]
    pub topic_name: String,
    #[sqlx(default)]
    pub topic_cli_reference: String,
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
