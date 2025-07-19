use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

#[derive(Clone, Debug, Serialize, Deserialize, Tabled)]
pub struct Section {
    chapter_title: String,
    chapter: f64,

    page_start: usize,
    page_exercises_start: usize,

    concepts_questions_interval_start: usize,
    concepts_questions_interval_end: usize,

    skills_questions_interval_start: usize,
    skills_questions_interval_end: usize,

    applications_questions_interval_start: usize,
    applications_questions_interval_end: usize,

    discussion_questions_interval_start: usize,
    discussion_questions_interval_end: usize,

    page_end: usize,

    #[serde(skip_deserializing)]
    book_title: String,

    #[serde(skip_deserializing)]
    book_author: String,

    #[serde(skip_deserializing)]
    book_reference: String,

    #[serde(skip_deserializing)]
    path: String,
}

impl Section {
    pub fn set_chapter_title(&mut self, chapter_title: String) {
        self.chapter_title = chapter_title;
    }

    pub fn chapter_title(&self) -> &str {
        &self.chapter_title
    }

    pub fn set_chapter(&mut self, chapter: f64) {
        self.chapter = chapter;
    }

    pub fn chapter(&self) -> &f64 {
        &self.chapter
    }

    pub fn set_page_start(&mut self, page_start: usize) {
        self.page_start = page_start;
    }

    pub fn page_start(&self) -> usize {
        self.page_start
    }

    pub fn set_page_exercises_start(&mut self, page_exercises_start: usize) {
        self.page_exercises_start = page_exercises_start;
    }

    pub fn page_exercises_start(&self) -> usize {
        self.page_exercises_start
    }

    pub fn set_concepts_questions_interval_start(
        &mut self,
        concepts_questions_interval_start: usize,
    ) {
        self.concepts_questions_interval_start = concepts_questions_interval_start;
    }

    pub fn concepts_questions_interval_start(&self) -> usize {
        self.concepts_questions_interval_start
    }

    pub fn set_concepts_questions_interval_end(&mut self, concepts_questions_interval_end: usize) {
        self.concepts_questions_interval_end = concepts_questions_interval_end;
    }

    pub fn concepts_questions_interval_end(&self) -> usize {
        self.concepts_questions_interval_end
    }

    pub fn set_skills_questions_interval_start(&mut self, skills_questions_interval_start: usize) {
        self.skills_questions_interval_start = skills_questions_interval_start;
    }

    pub fn skills_questions_interval_start(&self) -> usize {
        self.skills_questions_interval_start
    }

    pub fn set_skills_questions_interval_end(&mut self, skills_questions_interval_end: usize) {
        self.skills_questions_interval_end = skills_questions_interval_end;
    }

    pub fn skills_questions_interval_end(&self) -> usize {
        self.skills_questions_interval_end
    }

    pub fn set_applications_questions_interval_start(
        &mut self,
        applications_questions_interval_start: usize,
    ) {
        self.applications_questions_interval_start = applications_questions_interval_start;
    }

    pub fn applications_questions_interval_start(&self) -> usize {
        self.applications_questions_interval_start
    }

    pub fn set_applications_questions_interval_end(
        &mut self,
        applications_questions_interval_end: usize,
    ) {
        self.applications_questions_interval_end = applications_questions_interval_end;
    }

    pub fn applications_questions_interval_end(&self) -> usize {
        self.applications_questions_interval_end
    }

    pub fn set_discussion_questions_interval_start(
        &mut self,
        discussion_questions_interval_start: usize,
    ) {
        self.discussion_questions_interval_start = discussion_questions_interval_start;
    }

    pub fn discussion_questions_interval_start(&self) -> usize {
        self.discussion_questions_interval_start
    }

    pub fn discussion_questions_interval_end(&self) -> usize {
        self.discussion_questions_interval_end
    }

    pub fn set_discussion_questions_interval_end(
        &mut self,
        discussion_questions_interval_end: usize,
    ) {
        self.discussion_questions_interval_end = discussion_questions_interval_end;
    }

    pub fn set_page_end(&mut self, page_end: usize) {
        self.page_end = page_end;
    }

    pub fn page_end(&self) -> usize {
        self.page_end
    }

    pub fn book_title(&self) -> &str {
        &self.book_title
    }

    pub fn set_book_title(&mut self, book_title: String) {
        self.book_title = book_title;
    }

    pub fn book_author(&self) -> &str {
        &self.book_author
    }

    pub fn set_book_author(&mut self, book_author: String) {
        self.book_author = book_author;
    }

    pub fn book_reference(&self) -> &str {
        &self.book_reference
    }

    pub fn set_book_reference(&mut self, book_reference: String) {
        self.book_reference = book_reference;
    }

    pub fn set_path(&mut self, path: String) {
        self.path = path;
    }

    pub fn path(&self) -> &str {
        &self.path
    }
}
