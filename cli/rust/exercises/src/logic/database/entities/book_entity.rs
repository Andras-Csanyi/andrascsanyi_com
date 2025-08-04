use sqlx::FromRow;

#[derive(Debug, FromRow)]
pub struct BookEntity {
    pub id: i32,
    #[sqlx(default)]
    pub title: String,
    #[sqlx(default)]
    pub authors: String,
    pub page_start: i32,
    pub page_end: i32,
    pub reference: String,
    pub topic_id: i32,
}
