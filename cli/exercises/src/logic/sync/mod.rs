use super::structs::topic::Topic;
use welds::TransactStart;
use welds::errors::Result;

pub mod books_refresh;
pub mod chapters_refresh;
pub mod sections_refresh;
pub mod topic_refresh;

pub async fn execute(study_tree: Vec<Topic>) -> Result<()> {
    let connection_string = crate::logic::parsers::config::parse()
        .unwrap_or_else(|e| panic! {"Couldn't parse connection string. Error: {}", e});
    let client = welds::connections::connect(connection_string.database().url())
        .await
        .unwrap_or_else(|e| panic!("Couldn't create database client. Error: {}", e));

    let transaction = client
        .begin()
        .await
        .unwrap_or_else(|e| panic!("Couldn't start transaction. Error: {}", e));

    for topic in study_tree {
        println!("refresh topics: {}", topic.topic());
        topic_refresh::topic_refresh(&topic, &transaction).await?;
        books_refresh::topic_books_refresh(&topic, &transaction).await?;
        chapters_refresh::books_chapters_refresh(&topic, &transaction).await?;
        sections_refresh::chapters_sectons_refresh(&topic, &transaction).await?;
    }
    transaction
        .commit()
        .await
        .unwrap_or_else(|e| panic!("Transaction commit failed. Error: {}", e));
    Ok(())
}
