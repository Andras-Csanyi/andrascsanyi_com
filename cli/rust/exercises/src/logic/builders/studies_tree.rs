use std::path::Path;
use std::path::PathBuf;

use clap::Error;

use crate::logic::collectors::collector;
use crate::logic::parsers;
use crate::logic::structs::topic::Topic;

pub fn build(path: &Path) -> Result<Vec<Topic>, Error> {
    let files = collector::collect_recursively(path)?;
    let mut topics = parsers::topics::parse(files.clone())?;
    let books = parsers::books::parse(files.clone())?;
    let chapters = parsers::chapters::parse(files.clone())?;
    let sections = parsers::sections::parse(files.clone())?;

    topics.iter_mut().for_each(|topic| {
        let topic_path = PathBuf::from(topic.path())
            .parent()
            .map(|p| p.to_path_buf())
            .unwrap_or_else(|| panic!("Couldn't create parent based on topic path."));
        books.clone().into_iter().for_each(|book| {
            if PathBuf::from(book.path()).starts_with(topic_path.clone()) {
                topic.books_mut().push(book);
            }
        });
    });

    topics.iter_mut().for_each(|topic| {
        topic.books_mut().into_iter().for_each(|book| {
            let book_path = PathBuf::from(book.clone().path())
                .parent()
                .map(|p| p.to_path_buf())
                .unwrap_or_else(|| panic!("Couldn't create parent based on book path."));
            chapters.clone().into_iter().for_each(|chapter| {
                if PathBuf::from(chapter.clone().path()).starts_with(book_path.clone()) {
                    book.chapters_mut().push(chapter.clone());
                }
            });
        });
    });

    topics.iter_mut().for_each(|topic| {
        topic.books_mut().into_iter().for_each(|book| {
            book.chapters_mut().into_iter().for_each(|chapter| {
                let chapter_path = PathBuf::from(chapter.clone().path())
                    .parent()
                    .map(|p| p.to_path_buf())
                    .unwrap_or_else(|| panic!("Couldn't create parent based on chapter path."));
                sections.clone().into_iter().for_each(|section| {
                    if PathBuf::from(section.clone().path).starts_with(chapter_path.clone()) {
                        chapter.sections_mut().push(section.clone());
                    }
                });
            })
        });
    });

    Ok(topics)
}
