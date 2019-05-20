insert into [User](UserName, Email, CompletedQuizzes) values
	('craigstuckey', 'craigastuckey@gmail.com', 0),
	('franciscoguerrero', 'guerrerof615@gmail.com', 0),
	('rodrigosalomon', 'rodsalomon@utexas.edu', 0)

insert into GameMode(GameMode) values
	('Normal')

insert into Category(Category) values
	('Movies')

insert into Question(CategoryId, Value, Question) values
	(1, 10, 'Which war movie won the Academy Award for Best Picture in 2009?'),
	(1, 10, 'What was the name of the second Indiana Jones movie, released in 1984?'),
	(1, 10, 'Which actor starred in the 1961 movie The Hustler?'),
	(1, 10, 'In which year were the Academy Awards, or "Oscars", first presented?'),
	(1, 10, '"After all, tomorrow is another day!" is the last line from which movie that won the Academy Award for Best Picture in 1939?'),
	(1, 10, 'Which movie features Bruce Willis as John McClane, a New York police officer, taking on a gang of criminals in a Los Angeles skyscraper on Christmas Eve?'),
	(1, 10, 'What is the name of the hobbit played by Elijah Wood in the Lord of the Rings movies?'),
	(1, 10, 'Which actress plays Katniss Everdeen in the Hunger Games movies?'),
	(1, 10, 'Judy Garland starred as Dorothy Gale in which classic movie?'),
	(1, 10, 'What is the name of the kingdom where the 2013 animated movie Frozen is set?')

insert into Choice(QuestionId, Correct, Choice) values
	(1, 1, 'The Hurt Locker'),
	(1, 0, 'Avatar'),
	(1, 0, 'Zombieland'),
	(1, 0, 'Inglourious Bastards'),
	(2, 1, 'Indiana Jones and the Temple of Doom'),
	(2, 0, 'Raiders of the Lost Ark'),
	(2, 0, 'Indiana Jones and the Last Crusade'),
	(2, 0, 'Indiana Jones and the Kingdom of the Crystal Skull'),
	(3, 1, 'Paul Newman'),
	(3, 0, 'Jack Nicholson'),
	(3, 0, 'Rodney Dangerfield'),
	(3, 0, 'Bill Murray'),
	(4, 1, '1929'),
	(4, 0, '1943'),
	(4, 0, '1920'),
	(4, 0, '1938'),
	(5, 1, 'Gone with the Wind'),
	(5, 0, 'The Wizard of Oz'),
	(5, 0, 'Stagecoach'),
	(5, 0, 'Of Mice and Men'),
	(6, 1, 'Die Hard'),
	(6, 0, 'Hudson Hawk'),
	(6, 0, 'Pulp Fiction'),
	(6, 0, '12 Monkeys'),
	(7, 1, 'Frodo Baggins'),
	(7, 0, 'Samwise Gamgee'),
	(7, 0, 'Peregrin Took'),
	(7, 0, 'Bilbo Baggins'),
	(8, 1, 'Jennifer Lawrence'),
	(8, 0, 'Aubrey Plaza'),
	(8, 0, 'Rachel McAdams'),
	(8, 0, 'Scarlett Johansson'),
	(9, 1, 'The Wizard of Oz'),
	(9, 0, 'Meet Me in St. Louis'),
	(9, 0, 'A Star Is Born'),
	(9, 0, 'Easter Parade'),
	(10, 1, 'Arendelle'),
	(10, 0, 'Westeros'),
	(10, 0, 'Cyrodiil'),
	(10, 0, 'Azeroth')