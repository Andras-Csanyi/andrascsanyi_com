use sqlx::FromRow;

#[derive(Debug, FromRow)]
pub struct ChapterEntity {
    pub id: i32,
    #[sqlx(default)]
    pub title: String,
    #[sqlx(default)]
    pub reference: String,
    #[sqlx(default)]
    pub page_start: i32,
    #[sqlx(default)]
    pub page_end: i32,
    pub book_id: i32,
}
