use std::fmt;

#[derive(Clone, Debug)]
pub struct QuestionTmpl {
    book_title: String,
    chapter: String,
    chapter_exercises_start: usize,
    question_type: QuestionType,
    question_number: usize,
}

impl QuestionTmpl {
    pub fn new(
        book_title: String,
        chapter: String,
        chapter_exercises_start: usize,
        question_type: QuestionType,
        question_number: usize,
    ) -> Self {
        Self {
            book_title,
            chapter,
            chapter_exercises_start: chapter_exercises_start,
            question_type,
            question_number,
        }
    }

    pub fn chapter(&self) -> &str {
        &self.chapter
    }

    pub fn set_chapter(&mut self, chapter: String) {
        self.chapter = chapter;
    }

    pub fn chapter_exercises_start(&self) -> usize {
        self.chapter_exercises_start
    }

    pub fn set_chapter_exercises_start(&mut self, chapter_page_start: usize) {
        self.chapter_exercises_start = chapter_page_start;
    }

    pub fn question_type(&self) -> &QuestionType {
        &self.question_type
    }

    pub fn set_question_type(&mut self, question_type: QuestionType) {
        self.question_type = question_type;
    }

    pub fn set_question_number(&mut self, question_number: usize) {
        self.question_number = question_number;
    }

    pub fn question_number(&self) -> usize {
        self.question_number
    }

    pub fn book(&self) -> &str {
        &self.book_title
    }

    pub fn set_book(&mut self, book: String) {
        self.book_title = book;
    }
}

#[derive(Clone, Debug)]
pub enum QuestionType {
    Concept,
    Skill,
    Application,
    Discussion,
}

impl fmt::Display for QuestionType {
    fn fmt(&self, f: &mut fmt::Formatter) -> fmt::Result {
        match self {
            QuestionType::Concept => write!(f, "Concept"),
            QuestionType::Skill => write!(f, "Skill"),
            QuestionType::Application => write!(f, "Application"),
            QuestionType::Discussion => write!(f, "Discussion"),
        }
    }
}
