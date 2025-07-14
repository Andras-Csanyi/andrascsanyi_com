use serde::Deserialize;
use serde::Serialize;
use tabled::Tabled;

#[derive(Debug, Serialize, Deserialize, Tabled)]
pub struct Chapter {
    #[tabled(rename = "Title")]
    title: String,

    #[tabled(rename = "From")]
    page_start: u32,

    #[tabled(rename = "To")]
    page_end: u32,
}

impl Chapter {
    pub fn new(title: String, page_start: u32, page_end: u32) -> Self {
        Self {
            title,
            page_start,
            page_end,
        }
    }

    pub fn set_title(&mut self, title: String) {
        self.title = title;
    }

    pub fn title(&self) -> &str {
        &self.title
    }

    pub fn set_page_start(&mut self, page_start: u32) {
        self.page_start = page_start;
    }

    pub fn page_start(&self) -> u32 {
        self.page_start
    }

    pub fn set_page_end(&mut self, page_end: u32) {
        self.page_end = page_end;
    }

    pub fn page_end(&self) -> u32 {
        self.page_end
    }
}
