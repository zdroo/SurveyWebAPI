CREATE TABLE Surveys(
survey_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
name varchar(20),
description varchar(100)
);

INSERT INTO Surveys VALUES('Football Survey', 'Survey about football');

CREATE TABLE Users(
u_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
name varchar (30),
age INT
); 

CREATE TABLE Responses(
u_id INT,
q_id varchar(10),
response varchar(50)
);


CREATE TABLE Answers(
id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
ans_text varchar (20),
chosen INT DEFAULT 0,
q_id varchar(10),
survey_id INT,
FOREIGN KEY(survey_id) REFERENCES Surveys(survey_id) ON DELETE SET NULL
);

CREATE TABLE Questions(
q_id varchar(10) PRIMARY KEY,
qtext varchar(50),
qtype varchar(50),
survey_id INT,
FOREIGN KEY(survey_id) REFERENCES Surveys(survey_id) ON DELETE SET NULL
);



ALTER TABLE Questions ADD FOREIGN KEY(survey_id) REFERENCES Surveys(survey_id);
ALTER TABLE Answers ADD FOREIGN KEY(q_id) REFERENCES Questions(q_id);

ALTER TABLE Questions
ADD CHECK (qtype in ('single','multi','opentext','grid','opentextlist'));


INSERT INTO Questions VALUES('q1','What is your name?','opentext',1);
INSERT INTO Questions VALUES('q2','Do you like football','single',1);
INSERT INTO Questions VALUES('q3','Did you hear about these teams?','multi',1);

INSERT INTO Answers(ans_text, q_id, survey_id) VALUES('Yes','q1',1);
INSERT INTO Answers(ans_text, q_id, survey_id) VALUES('No','q1',1);
INSERT INTO Answers(q_id,survey_id) VALUES('q1',1);
INSERT INTO Answers(ans_text,q_id,survey_id) VALUES('Yes','q2',1);
INSERT INTO Answers(ans_text,q_id,survey_id) VALUES('No','q2',1);
INSERT INTO Answers(ans_text,q_id,survey_id) VALUES('Barcelona','q3',1);
INSERT INTO Answers(ans_text,q_id,survey_id) VALUES('Steaua','q3',1);
INSERT INTO Answers(ans_text,q_id,survey_id) VALUES('Dinamo','q3',1);

INSERT INTO Users(name,age) VALUES('Alex',23);
