use chrono::DateTime;
use chrono::Local;
use clap::Error;

pub fn provide() -> Result<String, Error> {
    let now: DateTime<Local> = Local::now();
    Ok(now.format("%Y-%m-%d_%H-%M-%S").to_string())
}
