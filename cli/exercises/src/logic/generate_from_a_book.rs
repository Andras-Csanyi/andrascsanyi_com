use std::collections::HashMap;
use std::path::Path;

use rand::Rng;

use crate::logic::get_chapters_of_a_book;
use crate::logic::get_sections_of_a_chapter;
use crate::logic::providers;
use crate::logic::renderers;
use crate::structs::chapter::Chapter;
use crate::structs::question_tmpl::QuestionTmpl;
use crate::structs::question_tmpl::QuestionType;
use crate::structs::section::Section;

use super::get_list_of_books;

pub fn execute(args: HashMap<String, String>, path: &Path) {
    let book_reference = args
        .get("book")
        .unwrap_or_else(|| panic!("No book reference was provided."));
    let books = get_list_of_books::execute(path)
        .unwrap_or_else(|e| panic!("Failed parsing the books and returned error: {}", e));
    let target_book = books
        .into_iter()
        .find(|b| b.reference() == book_reference)
        .unwrap_or_else(|| panic!("There is no book with reference: {}", book_reference));
    // println!("target book: {:#?}", target_book);
    // here we work with a single book!
    let mut available_chapters = get_chapters_of_a_book::execute(Path::new(target_book.path()))
        .unwrap_or_else(|e| {
            panic!(
                "Error happened while collecting chapters of {} book. Error: {}",
                target_book.title(),
                e
            )
        });
    // adding the book details for the chapters
    available_chapters.iter_mut().for_each(|i| {
        i.set_book_title(target_book.title().to_string());
        i.set_book_author(target_book.authors().to_string());
        i.set_book_reference(target_book.reference().to_string());
    });
    // println!("available chapters: {:#?}", available_chapters);
    let requested_chapters: Vec<String> = args
        .get("chapters")
        .expect("There is no value under the chapters key")
        .clone()
        .split(",")
        .map(|s| s.to_string())
        .collect();
    let chapters: Vec<&Chapter> = available_chapters
        .iter()
        .filter(|c| {
            requested_chapters
                .iter()
                .any(|r| r.as_str() == c.reference())
        })
        .collect();
    // println!("chapters: {:#?}", chapters);
    let mut sections: Vec<Section> = Vec::new();
    for ch in chapters.into_iter() {
        match get_sections_of_a_chapter::execute(ch.clone()) {
            Ok(mut parsed_sections) => {
                parsed_sections.iter_mut().for_each(|i| {
                    i.set_book_title(ch.book_title().to_string());
                    i.set_book_author(ch.book_author().to_string());
                    i.set_book_reference(ch.book_reference().to_string());
                });
                sections.extend(parsed_sections)
            }
            Err(e) => panic!(
                "Error happened while collecting sections of {} chapter. Error: {}",
                ch.reference(),
                e
            ),
        }
    }
    let mut final_questions: Vec<QuestionTmpl> = Vec::new();
    let mut random = rand::rng();

    // concept questions
    match args.get("concept") {
        Some(concept_questions_amount) => {
            let concept_question_vol: usize =
                concept_questions_amount.parse().unwrap_or_else(|e| {
                    panic!(
                        "Cannot parse concept volume {}, Error: {}",
                        concept_questions_amount, e
                    )
                });
            // this will be truncated and not rounded, but so far so good
            let concept_questions_per_section = sections.len() / concept_question_vol;
            sections.iter().for_each(|single_section| {
                for _q in 0..concept_questions_per_section {
                    let question_number = random.random_range(
                        single_section.concepts_questions_interval_start()
                            ..=single_section.concepts_questions_interval_end(),
                    );
                    let question = QuestionTmpl::new(
                        single_section.book_title().to_string(),
                        single_section.chapter().to_string(),
                        single_section.page_exercises_start(),
                        QuestionType::Concept,
                        question_number,
                    );
                    final_questions.push(question);
                }
            });
        }
        None => {
            println!("No concepts volume was provided, skipping concept questions");
        }
    };

    // skill questions
    match args.get("skill") {
        Some(skill_question_amount) => {
            let skill_question_vol: usize = skill_question_amount.parse().unwrap_or_else(|e| {
                panic!(
                    "Cannot parse skill questions volume {}, Error: {}",
                    skill_question_amount, e
                )
            });
            let skill_q_per_section = sections.len() / skill_question_vol;
            sections.iter().for_each(|single_section| {
                for _q in 0..skill_q_per_section {
                    let q_number = random.random_range(
                        single_section.skills_questions_interval_start()
                            ..=single_section.skills_questions_interval_end(),
                    );
                    let skill_question = QuestionTmpl::new(
                        single_section.book_title().to_string(),
                        single_section.chapter().to_string(),
                        single_section.page_exercises_start(),
                        QuestionType::Skill,
                        q_number,
                    );
                    final_questions.push(skill_question);
                }
            });
        }
        None => {
            println!("No skill questions volume was provided, skipping skill questions");
        }
    }

    // applications questions
    match args.get("application") {
        Some(applications_question_amount) => {
            let applications_question_vol: usize =
                applications_question_amount.parse().unwrap_or_else(|e| {
                    panic!(
                        "Cannot parse applications questions volume {}, Error: {}",
                        applications_question_amount, e
                    )
                });
            let app_q_per_section = sections.len() / applications_question_vol;
            sections.iter().for_each(|single_section| {
                for _q in 0..app_q_per_section {
                    let q_number = random.random_range(
                        single_section.applications_questions_interval_start()
                            ..=single_section.applications_questions_interval_end(),
                    );
                    let application_question = QuestionTmpl::new(
                        single_section.book_title().to_string(),
                        single_section.chapter().to_string(),
                        single_section.page_exercises_start(),
                        QuestionType::Application,
                        q_number,
                    );
                    final_questions.push(application_question);
                }
            });
        }
        None => {
            println!("No application questions volume was provided, skipping skill questions");
        }
    }

    // applications questions
    match args.get("discussion") {
        Some(disc_question_amount) => {
            let disc_question_vol: usize = disc_question_amount.parse().unwrap_or_else(|e| {
                panic!(
                    "Cannot parse applications questions volume {}, Error: {}",
                    disc_question_amount, e
                )
            });
            let disc_q_per_section = sections.len() / disc_question_vol;
            sections.iter().for_each(|single_section| {
                for _q in 0..disc_q_per_section {
                    let q_number = random.random_range(
                        single_section.discussion_questions_interval_start()
                            ..=single_section.discussion_questions_interval_end(),
                    );
                    let disc_question = QuestionTmpl::new(
                        single_section.book_title().to_string(),
                        single_section.chapter().to_string(),
                        single_section.page_exercises_start(),
                        QuestionType::Discussion,
                        q_number,
                    );
                    final_questions.push(disc_question);
                }
            });
        }
        None => {
            println!("No discussion questions volume was provided, skipping skill questions");
        }
    }

    let filename = providers::filename::provide()
        .unwrap_or_else(|e| panic!("Failed to provide the filename. Error: {}", e));
    renderers::pdf::execute(final_questions, filename);
}
