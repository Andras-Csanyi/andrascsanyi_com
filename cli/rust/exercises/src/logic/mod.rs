use std::fmt::Display;

use anyhow::Error;
use sqlx::PgPool;
use sqlx::postgres::PgPoolOptions;

pub mod arg_collectors;
pub mod builders;
pub mod collectors;
pub mod controllers;
pub mod database;
pub mod parsers;
pub mod providers;
pub mod renderers;
pub mod structs;
pub mod sync;

#[derive(Debug)]
pub enum ExercisesError {
    DatabaseError(String, String),
}

async fn create_connection() -> Result<PgPool, Error> {
    let connection_string = crate::logic::parsers::config::parse()
        .unwrap_or_else(|e| panic! {"Couldn't parse connection string. Error: {}", e});
    let connection = PgPoolOptions::new()
        .max_connections(5)
        .connect(&connection_string.database().url())
        .await
        .unwrap_or_else(|e| panic!("Couldn't create database client. Error: {}", e));
    Ok(connection)
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
