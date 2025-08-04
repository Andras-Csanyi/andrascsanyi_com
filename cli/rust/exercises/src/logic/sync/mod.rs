use anyhow::Error;
use sqlx::PgPool;

use super::structs::topic::Topic;

pub mod books_refresh;
pub mod chapters_refresh;
pub mod sections_refresh;
pub mod topic_refresh;

pub async fn execute(study_tree: Vec<Topic>, db_connection: &PgPool) -> Result<(), Error> {
    let mut transaction = db_connection.begin().await?;

    for topic in study_tree {
        println!("refresh topics: {}", topic.topic_name());
        topic_refresh::topic_refresh(&topic, &mut transaction).await?;
        books_refresh::topic_books_refresh(&topic, &mut transaction).await?;
        chapters_refresh::books_chapters_refresh(&topic, &transaction).await?;
        sections_refresh::chapters_sectons_refresh(&topic, &transaction).await?;
    }
    transaction
        .commit()
        .await
        .unwrap_or_else(|e| panic!("Transaction commit failed. Error: {}", e));
    Ok(())
}
