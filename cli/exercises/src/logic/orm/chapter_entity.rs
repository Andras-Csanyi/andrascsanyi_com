use welds::WeldsModel;

#[derive(Debug, WeldsModel)]
#[welds(table = "chapters")]
pub struct ChapterEntity {
    #[welds(primary_key)]
    pub id: i32,
    pub title: String,
    pub reference: String,
    pub page_start: i32,
    pub page_end: i32,
    pub book_id: i32,
}
