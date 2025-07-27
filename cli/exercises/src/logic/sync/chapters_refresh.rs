use welds::Client;
use welds::WeldsError;
use welds::errors::Result;

use crate::logic::orm::book::BookEntity;
use crate::logic::orm::chapter_entity::ChapterEntity;
use crate::logic::structs::topic::Topic;

pub async fn books_chapters_refresh(actual_topic: &Topic, transaction: &impl Client) -> Result<()> {
    for book_from_fs in actual_topic.books() {
        let book_id = BookEntity::all()
            .limit(1)
            .where_col(|i| i.reference.like(book_from_fs.reference()))
            .run(transaction)
            .await?;
        for chapter_from_fs in book_from_fs.chapters() {
            let mut chapter_from_db = ChapterEntity::all()
                .limit(1)
                .where_col(|i| i.reference.like(chapter_from_fs.reference()))
                .where_col(|i| i.book_id.equal(book_id.first().unwrap().id))
                .run(transaction)
                .await?;
            if chapter_from_db.is_empty() {
                let mut new_chapter_for_topic = ChapterEntity::new();
                new_chapter_for_topic.title = chapter_from_fs.title().to_string();
                new_chapter_for_topic.reference = chapter_from_fs.reference().to_string();
                new_chapter_for_topic.page_start = chapter_from_fs.page_start();
                new_chapter_for_topic.page_end = chapter_from_fs.page_end();
                new_chapter_for_topic.book_id = book_id.first().unwrap().id;
                new_chapter_for_topic.save(transaction).await?;
                continue;
            }
            if chapter_from_db.iter().count() > 1 {
                return Err(WeldsError::Other(anyhow::anyhow!(
                    "More than one chapter has been found with title: {} and book_id: {}",
                    chapter_from_fs.title(),
                    book_id.first().unwrap().id
                )));
            }
            if let Some(b) = chapter_from_db.first_mut() {
                b.title = chapter_from_fs.title().to_string();
                b.page_start = chapter_from_fs.page_start();
                b.page_end = chapter_from_fs.page_end();
                b.save(transaction).await?;
                continue;
            } else {
                return Err(WeldsError::Other(anyhow::anyhow!(
                    "There is no first element in the chapters list"
                )));
            }
        }
    }
    Ok(())
}
