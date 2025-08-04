DROP TABLE IF EXISTS sections;
DROP TABLE IF EXISTS chapters;
DROP TABLE IF EXISTS books;
DROP TABLE IF EXISTS topics;

\include topics.sql
\include books.sql
\include chapters.sql
\include sections.sql
