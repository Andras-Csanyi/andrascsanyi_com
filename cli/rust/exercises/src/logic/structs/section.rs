use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

#[derive(Clone, Debug, Serialize, Deserialize, Tabled)]
pub struct Section {
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
    #[serde(skip_deserializing)]
    pub book_title: String,
    #[serde(skip_deserializing)]
    pub book_author: String,
    #[serde(skip_deserializing)]
    pub book_reference: String,
    #[serde(skip_deserializing)]
    pub path: String,
}
