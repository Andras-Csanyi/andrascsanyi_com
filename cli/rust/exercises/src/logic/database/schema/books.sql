CREATE TABLE IF NOT EXISTS books (
    id INT GENERATED ALWAYS AS IDENTITY,
    title VARCHAR(255) NOT NULL,
    authors VARCHAR(255) NOT NULL,
    reference VARCHAR(255) NOT NULL,
    page_start INT NOT NULL,
    page_end INT NOT NULL,
    topic_id INT NOT NULL DEFAULT 0,
    PRIMARY KEY (id),

    CONSTRAINT fk_topic_id
    FOREIGN KEY (topic_id)
    REFERENCES topics (id)
);
