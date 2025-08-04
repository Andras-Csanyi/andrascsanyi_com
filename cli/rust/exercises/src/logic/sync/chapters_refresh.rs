use sqlx::Transaction;
use sqlx::query;
use sqlx::query_as;

use crate::logic::database::entities::book_entity::BookEntity;
use crate::logic::database::entities::chapter_entity::ChapterEntity;
use crate::logic::database::entities::topic_entity::TopicEntity;
use crate::logic::structs::topic::Topic;

pub async fn books_chapters_refresh(
    actual_topic: &Topic,
    transaction: &mut Transaction<'_, sqlx::Postgres>,
) -> anyhow::Result<(), anyhow::Error> {
    for book_from_fs in actual_topic.books() {
        let topic_id = query_as!(
            TopicEntity,
            r#"
            SELECT
                id
            FROM
                topics
            WHERE 
                topic_name = $1"#,
            actual_topic.topic_name()
        )
        .fetch_one(&mut **transaction)
        .await?;

        let book_id = query_as!(
            BookEntity,
            r#"
            SELECT
                id
            FROM
                books
            WHERE
                topic_id = $1
                AND reference = $2
            "#,
            topic_id.id,
            book_from_fs.reference()
        )
        .fetch_all(&mut **transaction)
        .await?;

        for chapter_from_fs in book_from_fs.chapters() {
            let chapter_from_db = query_as!(
                ChapterEntity,
                r#"
                SELECT
                    *
                FROM
                    chapters
                WHERE
                    book_id = $1
                    AND reference = $2
                "#,
                book_id.first().unwrap().id,
                chapter_from_fs.reference()
            )
            .fetch_all(&mut **transaction)
            .await?;

            if chapter_from_db.is_empty() {
                query!(
                    r#"
                INSERT INTO chapters (title, reference, page_start, page_end, book_id)
                VALUES ($1, $2, $3, $4, $5)
                "#,
                    chapter_from_fs.title(),
                    chapter_from_fs.reference(),
                    chapter_from_fs.page_start(),
                    chapter_from_fs.page_end(),
                    book_id.first().unwrap().id
                )
                .execute(&mut **transaction)
                .await?;
            }
            if chapter_from_db.iter().count() > 1 {
                return Err(anyhow::anyhow!(
                    "More than one chapter has been found with title: {} and book_id: {}",
                    chapter_from_fs.title(),
                    book_id.first().unwrap().id
                ));
            }
            query!(
                r#"
                UPDATE
                    chapters
                SET
                    title = $1,
                    reference = $2,
                    page_start = $3,
                    page_end = $4
            "#,
                chapter_from_fs.title(),
                chapter_from_fs.reference(),
                chapter_from_fs.page_start(),
                chapter_from_fs.page_end()
            )
            .execute(&mut **transaction)
            .await?;
        }
    }
    Ok(())
}
