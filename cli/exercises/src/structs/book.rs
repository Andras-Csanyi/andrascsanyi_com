use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

#[derive(Debug, Serialize, Deserialize, Tabled)]
pub struct Book {
    #[tabled(rename = "Title")]
    title: String,

    #[tabled(rename = "Authors")]
    authors: String,

    #[tabled(rename = "Page starts")]
    page_start: u32,

    #[tabled(rename = "Page ends")]
    page_end: u32,

    #[tabled(rename = "Ref")]
    reference: String,

    #[serde(skip_deserializing)]
    path: String,
}

impl Book {
    pub fn new(
        title: String,
        authors: String,
        page_start: u32,
        page_end: u32,
        reference: String,
        path: String,
    ) -> Self {
        Self {
            title,
            authors,
            page_start,
            page_end,
            reference,
            path,
        }
    }

    pub fn title(&self) -> &str {
        &self.title
    }

    pub fn set_title(&mut self, title: String) {
        self.title = title;
    }

    pub fn authors(&self) -> &str {
        &self.authors
    }

    pub fn set_authors(&mut self, authors: String) {
        self.authors = authors;
    }

    pub fn page_start(&self) -> u32 {
        self.page_start
    }

    pub fn set_page_start(&mut self, page_start: u32) {
        self.page_start = page_start;
    }

    pub fn page_end(&self) -> u32 {
        self.page_end
    }

    pub fn set_page_end(&mut self, page_end: u32) {
        self.page_end = page_end;
    }

    pub fn reference(&self) -> &str {
        &self.reference
    }

    pub fn set_reference(&mut self, reference: String) {
        self.reference = reference;
    }

    pub fn set_path(&mut self, path: String) {
        self.path = path;
    }

    pub fn path(&self) -> &str {
        &self.path
    }
}
