insert into Quiz(UserId,CategoryId, GameModeId,Score,[Time]) values
(2, 1, 1, 0, CURRENT_TIMESTAMP);

insert into Quiz(UserId,CategoryId, GameModeId,Score,[Time]) values
(1, 1, 1, 0, CURRENT_TIMESTAMP);

insert into Quiz(UserId,CategoryId, GameModeId,Score,[Time]) values
(3, 1, 1, 0, CURRENT_TIMESTAMP);

insert into QuizQuestion (QuizId, QuestionId) values
(1,1),
(2,2),
(3,3);

/*update [User]
set CompletedQuizzes=1; be careful if you run this again, changes all users completed quizzes*/

insert into Quiz(UserId,CategoryId,GameModeId,Score,[Time])values
(1,1,1,10,CURRENT_TIMESTAMP);

insert into QuizQuestion(QuizId, QuestionId) values
(4,6);

update [User]
set CompletedQuizzes=2
where [USER].UserId=1;

insert into Quiz(UserId,CategoryId,GameModeId,Score,[Time]) values
(9,2,1,10,CURRENT_TIMESTAMP);

insert into QuizQuestion(QuizId, QuestionId) values
(7,90);


update [User]
set CompletedQuizzes=1
where [USER].UserId=9;