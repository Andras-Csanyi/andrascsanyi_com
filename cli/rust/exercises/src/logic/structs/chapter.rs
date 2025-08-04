use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

use super::section::Section;

#[derive(Clone, Debug, Serialize, Deserialize, Tabled)]
pub struct Chapter {
    #[tabled(rename = "Title")]
    title: String,

    #[tabled(rename = "From")]
    page_start: i32,

    #[tabled(rename = "To")]
    page_end: i32,

    #[tabled(rename = "Ref")]
    reference: String,

    #[serde(skip_deserializing)]
    path: String,

    #[serde(skip_deserializing)]
    book_title: String,

    #[serde(skip_deserializing)]
    book_author: String,

    #[serde(skip_deserializing)]
    book_reference: String,

    #[serde(skip_deserializing)]
    #[tabled(skip)]
    sections: Vec<Section>,
}

impl Chapter {
    pub fn set_title(&mut self, title: String) {
        self.title = title;
    }

    pub fn title(&self) -> &str {
        &self.title
    }

    pub fn set_page_start(&mut self, page_start: i32) {
        self.page_start = page_start;
    }

    pub fn page_start(&self) -> i32 {
        self.page_start
    }

    pub fn set_page_end(&mut self, page_end: i32) {
        self.page_end = page_end;
    }

    pub fn page_end(&self) -> i32 {
        self.page_end
    }

    pub fn set_reference(&mut self, reference: String) {
        self.reference = reference;
    }

    pub fn reference(&self) -> &str {
        &self.reference
    }

    pub fn path(&self) -> &str {
        &self.path
    }

    pub fn set_path(&mut self, path: String) {
        self.path = path;
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

    pub fn set_book_reference(&mut self, book_reference: String) {
        self.book_reference = book_reference;
    }

    pub fn book_reference(&self) -> &str {
        &self.book_reference
    }

    pub fn sections(&self) -> &[Section] {
        &self.sections
    }

    pub fn sections_mut(&mut self) -> &mut Vec<Section> {
        &mut self.sections
    }

    pub fn set_sections(&mut self, sections: Vec<Section>) {
        self.sections = sections;
    }
}
