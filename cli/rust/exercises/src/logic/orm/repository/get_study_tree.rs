use welds::WeldsError;
use welds::connections::any::AnyClient;
use welds::prelude::*;

use crate::logic::orm::book::BookEntity;
use crate::logic::orm::topic::TopicEntity;
use crate::logic::orm::views::topic_book_chapter_section_question::TopicBookChapterSectionQuestion;

pub async fn get_study_tree(
    db_connection: &AnyClient,
) -> Result<Vec<TopicBookChapterSectionQuestion>, WeldsError> {
    println!("study tree");
    let query = TopicEntity::select(|topic| topic.id)
        .left_join::<BookEntity>("books", |topic, book| topic.id.equal(book.topic_id));
}
