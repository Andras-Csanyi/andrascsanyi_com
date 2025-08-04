use anyhow::Error;
use sqlx::Transaction;
use sqlx::query;
use sqlx::query_as;

use crate::logic::database::entities::topic_entity::TopicEntity;

pub async fn topic_refresh(
    topic: &crate::logic::structs::topic::Topic,
    transaction: &mut Transaction<'_, sqlx::Postgres>,
) -> Result<(), Error> {
    let topics_in_db = query_as!(
        TopicEntity,
        r#"
        SELECT
            *
        FROM topics
        WHERE
            topic_name = $1
        "#,
        topic.topic_name()
    )
    .fetch_all(&mut **transaction)
    .await?;
    if topics_in_db.is_empty() {
        query!(
            r#"
        INSERT INTO 
            topics (topic_name, topic_cli_reference)
        VALUES 
            ($1, $2)
        "#,
            topic.topic_name(),
            topic.topic_cli_reference()
        )
        .execute(&mut **transaction)
        .await?;

        return Ok(());
    }

    if topics_in_db.iter().count() > 1 {
        return Err(anyhow::anyhow!(
            "More than one hit for {} entity.",
            topic.topic_name()
        ));
    }

    let target_topic = topics_in_db.first().unwrap_or_else(|| {
        panic!("Couldn't take out the first, supposedly, single Topic element.")
    });

    query!(
        r#"
        UPDATE topics
        SET
            topic_name = $1,
            topic_cli_reference = $2
        WHERE
            id = $3
    "#,
        target_topic.topic_name,
        target_topic.topic_cli_reference,
        target_topic.id
    )
    .execute(&mut **transaction)
    .await?;

    Ok(())
}
