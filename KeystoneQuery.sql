DROP TABLE IF EXISTS UserSkills;
DROP TABLE IF EXISTS Tasks;
DROP TABLE IF EXISTS Projects;
DROP TABLE IF EXISTS Skills;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Statuses;
DROP TABLE IF EXISTS Priorities;
DROP TABLE IF EXISTS Roles;

GO

CREATE TABLE Roles (
	RoleID INT PRIMARY KEY IDENTITY(1,1),
	RoleName VARCHAR(50) NOT NULL UNIQUE,
	RoleDesc VARCHAR(200) NOT NULL,
	RolePermCreate BIT NOT NULL,
	RolePermEdit BIT NOT NULL,
	RolePermDelete BIT NOT NULL,
	RolePermInvite BIT NOT NULL,
	RolePermEditRoles BIT NOT NULL
);

CREATE TABLE Priorities (
	PriorityID INT PRIMARY KEY IDENTITY(1,1),
	PriorityName VARCHAR(50) NOT NULL UNIQUE,
	PriorityDesc VARCHAR(200) NOT NULL,
	PriorityColor VARCHAR(20) NOT NULL,
	PriorityLevel INT NOT NULL
);

CREATE TABLE Statuses (
	StatusID INT PRIMARY KEY IDENTITY(1,1),
	StatusName VARCHAR(50) NOT NULL UNIQUE,
	StatusDesc VARCHAR(200) NOT NULL,
	StatusColor VARCHAR(20) NOT NULL
);

CREATE TABLE Users (
	UserID INT PRIMARY KEY IDENTITY(1,1),
	UserFirstName VARCHAR(50) NOT NULL,
	UserLastName VARCHAR(50) NOT NULL,
	UserRole INT FOREIGN KEY REFERENCES Roles(RoleID) NOT NULL,
	UserEmail VARCHAR(100) NOT NULL UNIQUE,
	UserPassword VARCHAR(255) NOT NULL,
	UserPhone VARCHAR(20) NOT NULL,
	UserPFP VARBINARY(MAX) NULL
);

CREATE TABLE Skills (
	SkillID INT PRIMARY KEY IDENTITY(1, 1),
	SkillName VARCHAR(50) NOT NULL UNIQUE,
	SkillDesc VARCHAR(200) NOT NULL
);

CREATE TABLE Projects(
	ProjectID INT PRIMARY KEY IDENTITY(1, 1),
	ProjectName VARCHAR(50) NOT NULL,
	ProjectDesc VARCHAR(200) NOT NULL,
	ProjectOwnerUserID INT NOT NULL FOREIGN KEY REFERENCES Users (UserID),
	ProjectTimestamp DATETIME2 NULL
);

CREATE TABLE UserSkills (
	UserID INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
	SkillID INT NOT NULL FOREIGN KEY REFERENCES Skills(SkillID),
	PRIMARY KEY (UserID, SkillID)
);

CREATE TABLE Tasks (
	TaskID INT PRIMARY KEY IDENTITY(1,1),
	TaskName VARCHAR(50) NOT NULL,
	TaskDesc VARCHAR(200) NOT NULL,
	TaskPriority INT FOREIGN KEY REFERENCES Priorities(PriorityID) NOT NULL,
	TaskStatus INT FOREIGN KEY REFERENCES Statuses(StatusID) NOT NULL,
	TaskTimestamp DATETIME2 NULL, -- BASICALLY THE LAST EDIT
	TaskDueDate DATETIME NOT NULL,
	TaskProjectID INT FOREIGN KEY REFERENCES Projects(ProjectID) NOT NULL
);

GO

INSERT INTO Roles (RoleName, RoleDesc, RolePermCreate, RolePermEdit, RolePermDelete, RolePermInvite, RolePermEditRoles) VALUES
('Admin', 'Has all permissions', 1, 1, 1, 1, 1),
('Teammate', 'Can create and edit tasks', 1, 1, 0, 0, 0);
 
INSERT INTO Priorities (PriorityName, PriorityDesc, PriorityColor, PriorityLevel) VALUES
('Low', 'Low priority tasks', 'Green', 1),
('Medium', 'Medium priority tasks', 'Yellow', 2),
('High', 'High priority tasks', 'Red', 3);

INSERT INTO Statuses (StatusName, StatusDesc, StatusColor) VALUES
('To Do', 'Tasks that need to be done', 'Blue'),
('In Progress', 'Tasks that are currently being worked on', 'Orange'),
('Done', 'Tasks that have been completed', 'Green');

INSERT INTO Users (UserFirstName, UserLastName, UserRole, UserEmail, UserPassword, UserPhone, UserPFP) VALUES
('Tamer', 'Alssaleh', 1, 'tamerthegoat@eagles.usi.edu', 'ilovepink67', '555-555-5555', NULL),
('Will', 'Fardig', 2, 'wmfardig@eagles.usi.edu', 'keystonesux', '555-555-6767', NULL);

INSERT INTO Skills (SkillName, SkillDesc) VALUES
('C# Development', 'Proficient in C# programming and >NET'),
('Database Design', 'SQL Server design and optimization'),
('Front-End', 'HTML, CSS, JavaScript, Razor Pages'),
('Project Management', 'Agile methodologies and task tracking'),
('UI/UX Design', 'Creating user-friendly interfaces');

INSERT INTO UserSkills (UserID, SkillID) VALUES
(1, 1),
(1, 2),
(1, 4),
(2, 1),
(2, 3),
(2, 5);

INSERT INTO Projects(ProjectName, ProjectDesc, ProjectOwnerUserID, ProjectTimestamp) VALUES
('Midway Point Presentation', 'Class project presentation for Yan', 1, CURRENT_TIMESTAMP),
('Website Redesign', 'Update the company website with new features', 2, CURRENT_TIMESTAMP);

INSERT INTO Tasks (TaskName, TaskDesc, TaskPriority, TaskStatus, TaskTimestamp, TaskDueDate, TaskProjectID) VALUES
('Create Slides', 'Design 10-slide PowerPoint presentation', 2, 1, CURRENT_TIMESTAMP, '2026-04-20 12:00:00', 1),
('Write Script', 'Prepare speaking notes for presentation', 1, 1, CURRENT_TIMESTAMP, '2026-04-18 17:00:00', 1),
('Review Content', 'Final review before submission', 3, 3, CURRENT_TIMESTAMP, '2026-04-19 09:00:00', 1),
('Design Homepage', 'Create new homepage layout', 2, 2, CURRENT_TIMESTAMP, '2026-05-01 12:00:00', 2);
GO