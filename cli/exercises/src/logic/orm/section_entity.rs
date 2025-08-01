use welds::WeldsModel;

use super::chapter_entity::ChapterEntity;

#[derive(Debug, WeldsModel)]
#[welds(table = "sections")]
#[welds(BelongsTo(chapter, ChapterEntity, "chapter_id"))]
pub struct SectionEntity {
    #[welds(primary_key)]
    pub id: i32,
    pub section_title: String,
    pub section_number: f64,
    pub page_start: i32,
    pub page_exercises_start: i32,
    pub concepts_questions_interval_start: i32,
    pub concepts_questions_interval_end: i32,
    pub skills_questions_interval_start: i32,
    pub skills_questions_interval_end: i32,
    pub applications_questions_interval_start: i32,
    pub applications_questions_interval_end: i32,
    pub discussion_questions_interval_start: i32,
    pub discussion_questions_interval_end: i32,
    pub page_end: i32,
    pub chapter_id: i32,
}
