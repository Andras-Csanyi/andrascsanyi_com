CREATE TABLE IF NOT EXISTS chapters (
    id INT GENERATED ALWAYS AS IDENTITY,
    title VARCHAR(255) NOT NULL,
    reference VARCHAR(255) NOT NULL,
    page_start INTEGER NOT NULL,
    page_end INTEGER NOT NULL,
    book_id INT,
    PRIMARY KEY (id),

    CONSTRAINT fk_book_id
    FOREIGN KEY (book_id)
    REFERENCES books (id)
);
