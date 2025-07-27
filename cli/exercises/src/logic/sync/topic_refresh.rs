use welds::Client;
use welds::WeldsError;
use welds::errors::Result;

use crate::logic::orm::topic::TopicEntity;

pub async fn topic_refresh(
    topic: &crate::logic::structs::topic::Topic,
    transaction: &impl Client,
) -> Result<()> {
    let mut topic_hits = crate::logic::orm::topic::TopicEntity::all()
        .limit(1)
        .where_col(|i| i.topic.like(topic.topic()))
        .run(transaction)
        .await?;
    if topic_hits.is_empty() {
        let mut new_topic = TopicEntity::new();
        new_topic.topic = topic.topic().to_string();
        new_topic.topic_id = topic.topic_id().to_string();
        new_topic.save(transaction).await?;
        return Ok(());
    }

    if topic_hits.iter().count() > 1 {
        return Err(WeldsError::Other(anyhow::anyhow!(
            "More than one topic found. This should not happen."
        )));
    }

    if let Some(t) = topic_hits.first_mut() {
        t.topic = topic.topic().to_string();
        t.save(transaction).await?;
        Ok(())
    } else {
        return Err(WeldsError::Other(anyhow::anyhow!(
            "There is no first element in the topic list"
        )));
    }
}
