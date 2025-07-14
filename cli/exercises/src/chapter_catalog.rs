#[derive(Debug, Clone)]
struct ChapterCatalog {
    author: String,
    book: String,
    chapter_title: String,
    chapter: f32,

    page_start: i32,
    page_exercises_start: i32,

    concepts_questions_interval_start: i32,
    concepts_questions_interval_end: i32,

    skills_questions_interval_start: i32,
    skills_questions_interval_end: i32,

    applications_questions_interval_start: i32,
    applications_questions_interval_end: i32,

    discussion_questions_interval_start: i32,
    discussion_questions_interval_end: i32,

    page_end: i32,
}
