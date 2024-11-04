CREATE TABLE user_role (
	role_id INT NOT NULL,
	name VARCHAR(30) NOT NULL
	PRIMARY KEY (role_id)
);

INSERT INTO user_role (role_id, name)
VALUES (1, 'Patron'), (2, 'Librarian'), (3, 'Administrator');

CREATE TABLE user_info (
	user_id INT IDENTITY(1,1),
	username VARCHAR(100) NOT NULL UNIQUE,
	password VARCHAR(50) NOT NULL,
	role_id INT NOT NULL,
	PRIMARY KEY(user_id),
	CONSTRAINT fk_user_role_id FOREIGN KEY (role_id) REFERENCES user_role ON UPDATE CASCADE
);

INSERT INTO user_info (username, password, role_id)
VALUES ('user', '5F4DCC3B5AA765D61D8327DEB882CF99', 1), ('lib', '5F4DCC3B5AA765D61D8327DEB882CF99', 2); 


CREATE TABLE book (
	book_id INT IDENTITY(1,1),
	title VARCHAR(100) NOT NULL,
	description TEXT,
	author TEXT,
	publisher TEXT,
	publication_date VARCHAR(20),
	genre VARCHAR(50),
	isbn VARCHAR(13) NOT NULL UNIQUE,
	page_count INT,
	available BIT DEFAULT 1,
	PRIMARY KEY (book_id)
);

INSERT INTO book (title, description, author, publisher, publication_date, genre, isbn, page_count)
VALUES ('You Like It Darker: Stories', 'description for ISBN:9781668037713', 'Stephen King', 'Scribner', 'May 2024', 'genre for ISBN:9781668037713', '9781668037713', 512),
('11/22/63', 'description for ISBN:9781451627299', 'Stephen King', 'Gallery Books', '2012-08', 'genre for ISBN:9781451627299', '9781451627299', 849),
('Holly', 'description for ISBN:9781668016138', 'Stephen King', 'Scribner', '2023', 'genre for ISBN:9781668016138', '9781668016138', 666),
('Misery', 'description for ISBN:9781501143106', 'Stephen King', 'Scribner', '2016-01', 'genre for ISBN:9781501143106', '9781501143106', 351),
('Institute', 'description for ISBN:9781982110581', 'Stephen King', 'Gallery Books', '2020', 'genre for ISBN:9781982110581', '9781982110581', 576),
('Revival', 'description for ISBN:9781476770390', 'Stephen King', 'Gallery Books', '2014', 'genre for ISBN:9781476770390', '9781476770390', 540),
('The Outsider', 'description for ISBN:9781501181009', 'Stephen King', 'Simon and Schuster', '2018', 'genre for ISBN:9781501181009', '9781501181009', 561),
('Doctor Sleep', 'description for ISBN:9781476727653', 'Stephen King,José Óscar Hernández Sendin', 'Scribner', '2013-09', 'genre for ISBN:9781476727653', '9781476727653', 591),
('Carrie', 'description for ISBN:9780307743664', 'Stephen King', 'Anchor Books', '2011', 'genre for ISBN:9780307743664', '9780307743664', 304),
('On Writing', 'description for ISBN:9781982159375', 'Stephen King', 'Scribner', '2020', 'genre for ISBN:9781982159375', '9781982159375', 320),
('Oh, the places you''ll go!', 'description for ISBN:9780679805274', 'Dr. Seuss', 'Random House', '1990', 'genre for ISBN:9780679805274', '9780679805274', 48),
('The Lorax', 'description for ISBN:9780394823379', 'Seuss Dr.', 'Random House', '1971', 'genre for ISBN:9780394823379', '9780394823379', 70),
('Dr. Seuss''s If You Think There''s Nothing to Do', 'description for ISBN:9780593711361', 'Seuss', 'Random House Children''s Books', '2024', 'genre for ISBN:9780593711361', '9780593711361', 586),
('Happy birthday to you!', 'description for ISBN:9780394800769', 'Dr. Seuss', 'Random House', '1987', 'genre for ISBN:9780394800769', '9780394800769', 577),
('Fox in socks', 'description for ISBN:9780394800387', 'Seuss', 'Beginner Books, a division of Random House, Inc.', '1993', 'genre for ISBN:9780394800387', '9780394800387', 893),
('The foot book', 'description for ISBN:9780679882800', 'Dr. Seuss', 'Random House', '1996', 'genre for ISBN:9780679882800', '9780679882800', 656),
('Wacky Wednesday', 'description for ISBN:9780394829128', 'Dr. Seuss', 'Beginner Books,Random House Books for Young Readers', '1974', 'genre for ISBN:9780394829128', '9780394829128', 46),
('The Sneetches and other stories', 'description for ISBN:9780394800899', 'Dr. Seuss', 'Grolier', '1989', 'genre for ISBN:9780394800899', '9780394800899', 623),
('There''s a wocket in my pocket!', 'description for ISBN:9780394829203', 'Seuss', 'Beginner Books, a division of Random House, Inc.', '1974', 'genre for ISBN:9780394829203', '9780394829203', 563),
('Yertle the turtle and other stories', 'description for ISBN:9780394800875', 'Dr. Seuss', 'Random House,Random House Books for Young Readers', '1986', 'genre for ISBN:9780394800875', '9780394800875', 90),
('Dr. Seuss''s ABC (I Can Read It All By Myself Beginner Books)', 'description for ISBN:9780394800301', 'Dr. Seuss', 'Random House Books for Young Readers,Beginner Books', 'August 12, 1960', 'genre for ISBN:9780394800301', '9780394800301', 72),
('Dr. Seuss''s Book of Colors', 'description for ISBN:9781524766184', 'Dr. Seuss', 'Random House Books for Young Readers,AMERICAN WEST BOOKS', 'Jan 02, 2018', 'genre for ISBN:9781524766184', '9781524766184', 40),
('Mr Brown can moo! Can you?', 'description for ISBN:9780394806228', 'Dr. Seuss', 'Random House', '1970', 'genre for ISBN:9780394806228', '9780394806228', 27),
('Dr. Seuss''s Sleep Book (Classic Seuss)', 'description for ISBN:9780394800912', 'Dr. Seuss', 'Random House Books for Young Readers, Div of Random House, Inc.,simultaneously by Random House of Canada, Ltd.', 'August 12, 1962', 'genre for ISBN:9780394800912', '9780394800912', 64),
('What Pet Should I Get?', 'description for ISBN:9780525707356', 'Dr. Seuss', 'Random House Books for Young Readers', 'Jan 08, 2019', 'genre for ISBN:9780525707356', '9780525707356', 40),
('Dr. Seuss''s Thankful Things', 'description for ISBN:9780593302170', 'Dr. Seuss', 'Random House Books for Young Readers', 'Sep 07, 2021', 'genre for ISBN:9780593302170', '9780593302170', 26),
('Dr. Seuss''s Who Loves You?', 'description for ISBN:9780593648360', 'Seuss', 'Random House Children''s Books', '2023', 'genre for ISBN:9780593648360', '9780593648360', 804),
('Oh, the thinks you can think!', 'description for ISBN:9780394831299', 'Seuss', 'Beginner Books', '1975', 'genre for ISBN:9780394831299', '9780394831299', 843),
('Did I ever tell you how lucky you are?', 'description for ISBN:9780394827193', 'Dr. Seuss', 'Random House', '1973', 'genre for ISBN:9780394827193', '9780394827193', 47),
('Olivia', 'description for ISBN:9780689829536', 'Ian Falconer', 'Atheneum Books for Young Readers,Atheneum/Anne Schwartz Books', '2000', 'genre for ISBN:9780689829536', '9780689829536', 36);

CREATE TABLE book_loan (
	loan_id INT IDENTITY(1,1),
	user_id INT NOT NULL,
	book_id INT NOT NULL,
	checked_out DATETIME DEFAULT CURRENT_TIMESTAMP,
	checked_in DATETIME,
	PRIMARY KEY (loan_id),
	CONSTRAINT uk_book_loan_user_book UNIQUE (user_id, book_id),
	CONSTRAINT fk_book_loan_book_id FOREIGN KEY (book_id) REFERENCES book ON DELETE CASCADE
);

SELECT * from user_info
where user_id = 3;

INSERT INTO book_loan(user_id, book_id, checked_out)
VALUES (1, 1, CURRENT_TIMESTAMP),
(2, 3, CURRENT_TIMESTAMP),
(2, 4, CURRENT_TIMESTAMP),
(1, 10, CURRENT_TIMESTAMP);

CREATE TABLE user_review (
	user_review_id INT IDENTITY(1,1),
	user_id INT NOT NULL,
	book_id INT NOT NULL,
	customer_review TEXT,
	rating TINYINT, /*rating 0 - 10*/
	created_on DATETIME DEFAULT CURRENT_TIMESTAMP
	PRIMARY KEY (user_review_id),
	CONSTRAINT uk_book_review_user_book UNIQUE (user_id, book_id),
	CONSTRAINT fk_book_review_book_id FOREIGN KEY (book_id) REFERENCES book ON DELETE CASCADE,
	CONSTRAINT fk_book_review_user_id FOREIGN KEY (user_id) REFERENCES user_info ON DELETE CASCADE
);

INSERT INTO user_review(user_id, book_id, customer_review, rating)
VALUES (1, 7, 'great', 8),
(1, 25, 'poor', 2),
(2, 1, 'alright',5),
(2, 7, 'awful', 1),
(2, 10, 'love', 10);