use welds::WeldsModel;

#[derive(Debug, WeldsModel)]
#[welds(table = "topics")]
pub struct Topic {
    #[welds(primary_key)]
    pub id: i32,
    pub topic: String,
    pub topic_id: String,
}
