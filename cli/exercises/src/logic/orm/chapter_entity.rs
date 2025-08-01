use welds::WeldsModel;

use super::book::BookEntity;
use super::section_entity::SectionEntity;

#[derive(Debug, WeldsModel)]
#[welds(table = "chapters")]
#[welds(BelongsTo(book, BookEntity, "book_id"))]
#[welds(HasMany(sections, SectionEntity, "chapter_id"))]
pub struct ChapterEntity {
    #[welds(primary_key)]
    pub id: i32,
    pub title: String,
    pub reference: String,
    pub page_start: i32,
    pub page_end: i32,
    pub book_id: i32,
}
