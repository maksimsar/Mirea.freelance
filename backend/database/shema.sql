-- Создание базы данных
CREATE DATABASE freelance;

-- Подключение к базе данных
\c freelance;

-- Создание таблицы Feedback
CREATE TABLE Feedback (
    Id SERIAL PRIMARY KEY,
    TaskId INT NOT NULL,
    AuthorProfileId INT NOT NULL,
    RecipientProfileId INT NOT NULL,
    Rating NUMERIC(3, 2) CHECK (Rating >= 1 AND Rating <= 5),
    Comment TEXT,
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Создание таблицы Profile
CREATE TABLE Profile (
    UserId SERIAL PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Age INT NOT NULL,
    Gender VARCHAR(50) NOT NULL,
    Rating NUMERIC(3, 2) DEFAULT 0
);

-- Создание таблицы Role
CREATE TABLE Role (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL UNIQUE
);

-- Создание таблицы Task
CREATE TABLE Task (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Description TEXT NOT NULL,
    Status VARCHAR(50) DEFAULT 'Open',
    Budget NUMERIC(10, 2) NOT NULL,
    ClientProfileId INT NOT NULL REFERENCES Profile(UserId),
    FreelancerProfileId INT REFERENCES Profile(UserId),
    CreatedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Deadline TIMESTAMP
);

-- Создание таблицы User
CREATE TABLE "User" (
    Id SERIAL PRIMARY KEY,
    Login VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    RegistrationDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Создание таблицы UserRole
CREATE TABLE UserRole (
    Id SERIAL PRIMARY KEY,
    UserId INT NOT NULL REFERENCES "User"(Id),
    RoleId INT NOT NULL REFERENCES Role(Id),
    AssignedDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Установка внешних ключей для Feedback
ALTER TABLE Feedback
ADD CONSTRAINT FK_Feedback_Task FOREIGN KEY (TaskId) REFERENCES Task(Id),
ADD CONSTRAINT FK_Feedback_AuthorProfile FOREIGN KEY (AuthorProfileId) REFERENCES Profile(UserId),
ADD CONSTRAINT FK_Feedback_RecipientProfile FOREIGN KEY (RecipientProfileId) REFERENCES Profile(UserId);
