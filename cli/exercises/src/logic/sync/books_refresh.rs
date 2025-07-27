use welds::Client;
use welds::WeldsError;
use welds::errors::Result;

pub async fn topic_books_refresh(
    actual_topic: &crate::logic::structs::topic::Topic,
    transaction: &impl Client,
) -> Result<()> {
    for topic_book in actual_topic.books() {
        // get the id of the topic from the db
        // here we iterate through what we have on the FS which doesn't have the ids the db knows
        let topic_id = crate::logic::orm::topic::TopicEntity::all()
            .limit(1)
            .where_col(|i| i.topic.like(actual_topic.topic()))
            .run(transaction)
            .await?;
        // we are looking for the data in the database
        let mut book = crate::logic::orm::book::BookEntity::all()
            .limit(1)
            .where_col(|i| i.reference.like(topic_book.reference()))
            .where_col(|i| i.topic_id.equal(topic_id.first().unwrap().id))
            .run(transaction)
            .await?;
        if book.is_empty() {
            // adding the new book if it is not in the db
            let mut new_book_for_topic = crate::logic::orm::book::BookEntity::new();
            new_book_for_topic.title = topic_book.title().to_string();
            new_book_for_topic.authors = topic_book.authors().to_string();
            new_book_for_topic.page_start = topic_book.page_start();
            new_book_for_topic.page_end = topic_book.page_end();
            new_book_for_topic.reference = topic_book.reference().to_string();
            new_book_for_topic.topic_id = topic_id.first().unwrap().id;
            new_book_for_topic.save(transaction).await?;
            continue;
        }
        if book.iter().count() > 1 {
            return Err(WeldsError::Other(anyhow::anyhow!(
                "More than one book has been found with title: {} and topic_id: {}",
                topic_book.title(),
                topic_id.first().unwrap().id
            )));
        }
        if let Some(b) = book.first_mut() {
            b.title = topic_book.title().to_string();
            b.authors = topic_book.authors().to_string();
            b.page_start = topic_book.page_start();
            b.page_end = topic_book.page_end();
            b.save(transaction).await?;
            continue;
        } else {
            return Err(WeldsError::Other(anyhow::anyhow!(
                "There is no first element in the topic list"
            )));
        }
    }
    Ok(())
}
