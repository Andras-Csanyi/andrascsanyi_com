CREATE TABLE IF NOT EXISTS sections (
    id INT GENERATED ALWAYS AS IDENTITY,
    section_title VARCHAR(255) NOT NULL,
    section_number DOUBLE PRECISION NOT NULL,
    page_start INT,
    page_exercises_start INT,
    concepts_questions_interval_start INT,
    concepts_questions_interval_end INT,
    skills_questions_interval_start INT,
    skills_questions_interval_end INT,
    applications_questions_interval_start INT,
    applications_questions_interval_end INT,
    discussion_questions_interval_start INT,
    discussion_questions_interval_end INT,
    page_end INT,
    chapter_id INT,
    PRIMARY KEY (id),

    CONSTRAINT fk_chapter_id
    FOREIGN KEY (chapter_id)
    REFERENCES chapters (id)
);
