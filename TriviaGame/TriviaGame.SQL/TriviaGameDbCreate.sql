create table [User] (
	UserId int identity,
	UserName nvarchar(20) not null,
	Email nvarchar(100) not null,
	CompletedQuizzes int,
	constraint PK_UserId primary key(UserId)
)

create table Quiz (
	QuizId int identity,
	UserId int not null,
	CategoryId int not null,
	GameModeId int not null,
	Score int not null,
	Time datetime not null,
	constraint PK_QuizId primary key(QuizId)
)

create table Category (
	CategoryId int identity,
	Category nvarchar(20),
	constraint PK_CategoryId primary key(CategoryId)
)

create table GameMode (
	GameModeId int identity,
	GameMode nvarchar(100),
	constraint PK_GameModeId primary key(GameModeId)
)

create table Question (
	QuestionId int identity,
	CategoryId int not null,
	Question nvarchar(200) not null,
	Value int not null,
	constraint PK_QuestionId primary key(QuestionId)
)

create table QuizQuestion (
	QuizQuestionId int identity,
	QuizId int not null,
	QuestionId int not null,
	constraint PK_QuizQuestionId primary key(QuizQuestionId)
)	

create table Choice (
	ChoiceId int identity,
	QuestionId int not null,
	Correct bit,
	Choice nvarchar(100),
	constraint PK_ChoiceId primary key(ChoiceId)
)

alter table Quiz add
	constraint FK_UserId foreign key(UserId) references [User](UserId),
	constraint FK_CategoryId1 foreign key(CategoryId) references Category(CategoryId),
	constraint FK_GameModeId foreign key(GameModeId) references GameMode(GameModeId)

alter table QuizQuestion add
	constraint FK_QuizId foreign key(QuizId) references Quiz(QuizId),
	constraint FK_QuestionId1 foreign key(QuestionId) references Question(QuestionId)

alter table Choice add
	constraint FK_QuestionId2 foreign key(QuestionId) references Question(QuestionId)

alter table Question add
	constraint FK_CategoryId2 foreign key(CategoryId) references Category(CategoryId)