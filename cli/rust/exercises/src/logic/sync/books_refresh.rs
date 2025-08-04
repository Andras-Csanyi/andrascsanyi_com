use anyhow::Error;
use sqlx::Transaction;
use sqlx::query;
use sqlx::query_as;

use crate::logic::database::entities::book_entity::BookEntity;
use crate::logic::database::entities::topic_entity::TopicEntity;

pub async fn topic_books_refresh(
    actual_topic: &crate::logic::structs::topic::Topic,
    transaction: &mut Transaction<'_, sqlx::Postgres>,
) -> Result<(), Error> {
    for topic_book in actual_topic.books() {
        // get the id of the topic from the db
        // here we iterate through what we have on the FS which doesn't have the ids the db knows
        let topic_id = query_as!(
            TopicEntity,
            r#"
            SELECT
                id, topic_name, topic_cli_reference
            FROM 
                topics
            WHERE
                topic_name = $1
        "#,
            actual_topic.topic_name()
        )
        .fetch_one(&mut **transaction)
        .await?;
        // we are looking for the data in the database

        let books_in_topic = query_as!(
            BookEntity,
            r#"
                SELECT 
                    *
                FROM 
                    books
                WHERE 
                    topic_id = $1
                    AND  reference = $2
            "#,
            topic_id.id,
            topic_book.reference()
        )
        .fetch_all(&mut **transaction)
        .await?;

        if books_in_topic.is_empty() {
            query!(
                r#"
                INSERT INTO 
                    books (title, authors, reference, page_start, page_end, topic_id)
                VALUES 
                    ($1, $2, $3, $4, $5, $6)
            "#,
                topic_book.title(),
                topic_book.authors(),
                topic_book.reference(),
                topic_book.page_start(),
                topic_book.page_end(),
                topic_id.id
            )
            .execute(&mut **transaction)
            .await?;
        }

        if books_in_topic.iter().count() > 1 {
            return Err(anyhow::anyhow!(
                "There are more than one Books in the database with topic_id={} and reference={}",
                topic_id.id,
                topic_book.reference()
            ));
        }

        query!(
            r#"
            UPDATE 
                books
            SET 
                title = $1, 
                authors = $2, 
                reference = $3, 
                page_start = $4, 
                page_end = $5
            WHERE id = $6
        "#,
            topic_book.title(),
            topic_book.authors(),
            topic_book.reference(),
            topic_book.page_start(),
            topic_book.page_end(),
            topic_id.id
        )
        .execute(&mut **transaction)
        .await?;
    }
    Ok(())
}
