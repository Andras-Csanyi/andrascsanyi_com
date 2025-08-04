use std::process::Command;

use crate::logic::structs::question_tmpl::QuestionTmpl;

pub fn execute(questions: Vec<QuestionTmpl>, filename_partial: String) {
    let mut items: String = String::from("");
    questions.iter().for_each(|question| {
        let single_question = format!(
            r#"
            \paragraph{{}}
            \begin{{tabularx}}{{1\textwidth}}{{
                    p{{\dimexpr0.5\textwidth\relax}}
                    p{{\dimexpr0.5\textwidth\relax}}
                }}
                \toprule
                Book: & {book}
                \\
                \midrule
                Chapter: & {chapter}
                \\
                \midrule
                Page: & {page}
                \\
                \midrule
                Question number: & {question_number}
                \\
                \midrule
                Question type: & {question_type}
                \\
                \bottomrule
            \end{{tabularx}}
            "#,
            book = question.book().to_string(),
            chapter = question.chapter().to_string(),
            page = question.chapter_exercises_start().to_string(),
            question_number = question.question_number().to_string(),
            question_type = question.question_type().to_string()
        );
        items.push_str(&single_question);
    });
    let latex = format!(
        r#"
        \documentclass{{article}}
        \usepackage{{tabularx}}
        \usepackage{{booktabs}}
        \begin{{document}}
        {}
        \end{{document}}
        "#,
        items
    );
    let filename_tex = format!("{filename_partial}.tex");
    let filename_pdf = format!("{filename_partial}.pdf");

    match std::fs::write(&filename_tex, latex) {
        Ok(_) => {
            println!("File: {}, was written successfully.", &filename_tex);
        }
        Err(e) => {
            println!(
                "Error while writing the file: {}. Error: {}",
                &filename_tex, e
            );
        }
    }

    match Command::new("tectonic").arg("--version").output() {
        Ok(_) => {}
        Err(e) => {
            panic!("no tectonic on PATH, {}", e)
        }
    }

    match Command::new("pdflatex")
        .arg("--outfmt pdf")
        .arg(&filename_tex)
        .output()
    {
        Ok(_) => {
            println!("{} is generated successfully.", &filename_pdf)
        }
        Err(e) => {
            println!("Error while generating {}. Error: {}", &filename_pdf, e)
        }
    }
}
