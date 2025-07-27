use std::fmt::Display;

pub mod arg_collectors;
pub mod builders;
pub mod collectors;
pub mod controllers;
pub mod orm;
pub mod parsers;
pub mod providers;
pub mod renderers;
pub mod structs;
pub mod sync;

#[derive(Debug)]
pub enum ExercisesError {
    DatabaseError(String, String),
}

impl Display for ExercisesError {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        match self {
            ExercisesError::DatabaseError(reason, trace) => {
                write!(f, "Database Error: {}, details: {}", reason, trace)
            }
        }
    }
}

impl std::error::Error for ExercisesError {}
