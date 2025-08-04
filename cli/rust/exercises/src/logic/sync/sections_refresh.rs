use welds::Client;
use welds::WeldsError;
use welds::errors::Result;

use crate::logic::orm::book::BookEntity;
use crate::logic::orm::chapter_entity::ChapterEntity;
use crate::logic::orm::section_entity::SectionEntity;
use crate::logic::structs::topic::Topic;

pub async fn chapters_sectons_refresh(
    actual_topic: &Topic,
    transaction: &impl Client,
) -> Result<()> {
    for book_from_fs in actual_topic.books() {
        let book_id = BookEntity::all()
            .limit(1)
            .where_col(|i| i.reference.like(book_from_fs.reference()))
            .run(transaction)
            .await?;
        for chapter_from_fs in book_from_fs.chapters() {
            let chapter_id = ChapterEntity::all()
                .limit(1)
                .where_col(|i| i.reference.like(chapter_from_fs.reference()))
                .where_col(|i| i.book_id.equal(book_id.first().unwrap().id))
                .run(transaction)
                .await?;
            for section_from_fs in chapter_from_fs.sections() {
                let mut section_from_db = SectionEntity::all()
                    .limit(1)
                    .where_col(|i| i.section_title.like(section_from_fs.section_title.clone()))
                    .where_col(|i| i.chapter_id.equal(chapter_id.first().unwrap().id))
                    .run(transaction)
                    .await?;
                if section_from_db.is_empty() {
                    let mut new_section_for_topic = SectionEntity::new();
                    new_section_for_topic.section_title = section_from_fs.section_title.to_string();
                    new_section_for_topic.section_number = section_from_fs.section_number;
                    new_section_for_topic.page_start = section_from_fs.page_start;
                    new_section_for_topic.page_exercises_start =
                        section_from_fs.page_exercises_start;
                    new_section_for_topic.concepts_questions_interval_start =
                        section_from_fs.concepts_questions_interval_start;
                    new_section_for_topic.concepts_questions_interval_end =
                        section_from_fs.concepts_questions_interval_end;
                    new_section_for_topic.skills_questions_interval_start =
                        section_from_fs.skills_questions_interval_start;
                    new_section_for_topic.skills_questions_interval_end =
                        section_from_fs.skills_questions_interval_end;
                    new_section_for_topic.applications_questions_interval_start =
                        section_from_fs.applications_questions_interval_start;
                    new_section_for_topic.applications_questions_interval_end =
                        section_from_fs.applications_questions_interval_end;
                    new_section_for_topic.discussion_questions_interval_start =
                        section_from_fs.discussion_questions_interval_start;
                    new_section_for_topic.discussion_questions_interval_end =
                        section_from_fs.discussion_questions_interval_end;
                    new_section_for_topic.page_end = section_from_fs.page_end;
                    new_section_for_topic.chapter_id = chapter_id.first().unwrap().id;
                    new_section_for_topic.save(transaction).await?;
                    continue;
                }
                if section_from_db.iter().count() > 1 {
                    return Err(WeldsError::Other(anyhow::anyhow!(
                        "More than one section has been found with title: {} and chapter_id: {}",
                        section_from_fs.section_title,
                        chapter_id.first().unwrap().id
                    )));
                }
                if let Some(b) = section_from_db.first_mut() {
                    b.section_title = section_from_fs.section_title.to_string();
                    b.section_number = section_from_fs.section_number;
                    b.page_start = section_from_fs.page_start;
                    b.page_exercises_start = section_from_fs.page_exercises_start;
                    b.concepts_questions_interval_start =
                        section_from_fs.concepts_questions_interval_start;
                    b.concepts_questions_interval_end =
                        section_from_fs.concepts_questions_interval_end;
                    b.skills_questions_interval_start =
                        section_from_fs.skills_questions_interval_start;
                    b.skills_questions_interval_end = section_from_fs.skills_questions_interval_end;
                    b.applications_questions_interval_start =
                        section_from_fs.applications_questions_interval_start;
                    b.applications_questions_interval_end =
                        section_from_fs.applications_questions_interval_end;
                    b.discussion_questions_interval_start =
                        section_from_fs.discussion_questions_interval_start;
                    b.discussion_questions_interval_end =
                        section_from_fs.discussion_questions_interval_end;
                    b.page_end = section_from_fs.page_end;
                    b.save(transaction).await?;
                    continue;
                } else {
                    return Err(WeldsError::Other(anyhow::anyhow!(
                        "There is no first element in the chapters list"
                    )));
                }
            }
        }
    }
    Ok(())
}
