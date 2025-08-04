use clap::Error;
use config::Config;
use config::File;

pub fn parse() -> Result<crate::logic::structs::config::Config, Error> {
    match Config::builder()
        .add_source(File::with_name("exercises.config.toml"))
        .build()
    {
        Ok(r) => match r.try_deserialize::<crate::logic::structs::config::Config>() {
            Ok(res) => Ok(res),
            Err(e) => {
                panic!(
                    "Failed to parse the exercises.config.toml file. Error: {}",
                    e
                )
            }
        },
        Err(e) => {
            panic!(
                "Failed to parse the exercises.config.toml file. Error: {}",
                e
            )
        }
    }
}
