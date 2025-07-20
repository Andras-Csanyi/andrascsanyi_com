CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    author VARCHAR(255) NOT NULL,
    reference VARCHAR(255) NOT NULL,
    page_start INTEGER NOT NULL,
    page_end INTEGER NOT NULL
);
