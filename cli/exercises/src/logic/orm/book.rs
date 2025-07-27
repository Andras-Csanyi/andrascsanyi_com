use welds::WeldsModel;

#[derive(Debug, WeldsModel)]
#[welds(table = "books")]
pub struct BookEntity {
    #[welds(primary_key)]
    pub id: i32,
    pub title: String,
    pub authors: String,
    pub page_start: i32,
    pub page_end: i32,
    pub reference: String,
    pub topic_id: i32,
}
