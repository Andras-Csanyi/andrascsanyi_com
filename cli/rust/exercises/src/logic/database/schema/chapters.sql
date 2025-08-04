CREATE TABLE IF NOT EXISTS chapters (
    id INT GENERATED ALWAYS AS IDENTITY,
    title VARCHAR(255) NOT NULL,
    reference VARCHAR(255) NOT NULL,
    page_start INT NOT NULL,
    page_end INT NOT NULL,
    book_id INT NOT NULL DEFAULT 0,
    PRIMARY KEY (id),

    CONSTRAINT fk_book_id
    FOREIGN KEY (book_id)
    REFERENCES books (id)
);
