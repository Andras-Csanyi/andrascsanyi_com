use clap::Error;
use welds::TransactStart;

use crate::logic::orm::topic::Topic;
use crate::logic::parsers::config;

pub async fn execute(study_tree: Vec<crate::logic::structs::topic::Topic>) -> Result<(), Error> {
    println!("study tree start");
    let connection_string = config::parse()
        .unwrap_or_else(|e| panic! {"Couldn't parse connection string. Error: {}", e});
    let client = welds::connections::connect(connection_string.database().url())
        .await
        .unwrap_or_else(|e| panic!("Couldn't create database client. Error: {}", e));

    let transaction = client
        .begin()
        .await
        .unwrap_or_else(|e| panic!("Couldn't start transaction. Error: {}", e));

    for topic in study_tree {
        match crate::logic::orm::topic::Topic::all()
            .limit(1)
            .where_col(|i| i.topic.like(topic.topic()))
            .run(&client)
            .await
        {
            Ok(hit) => {
                if hit.len() == 0 {
                    let mut new_topic = Topic::new();
                    new_topic.topic = topic.topic().to_string();
                    new_topic.topic_id = topic.topic_id().to_string();
                    match new_topic.save(&client).await {
                        Ok(saved) => {
                            println!("saved: {:#?}", saved);
                        }
                        Err(e) => {
                            transaction.rollback().await.unwrap_or_else(|e| {
                                panic!("Couldn't rollback transaction. Error: {}", e)
                            });
                            panic!("Couldn't save topic. Error: {}", e)
                        }
                    }
                }
            }
            Err(e) => {
                panic!("Weld couldn't execute query. Error: {}", e);
            }
        }
    }
    transaction
        .commit()
        .await
        .unwrap_or_else(|e| panic!("Transaction commit failed. Error: {}", e));

    Ok(())
}
