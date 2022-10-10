
CREATE TABLE dbo.[Users]
(
    ID INT IDENTITY(1,1) NOT NULL,
    Username VARCHAR(40) NOT NULL,
    PasswordHash BINARY(64) NOT NULL,
    FirstName VARCHAR(40) NULL,
    LastName VARCHAR(40) NULL,
	IsAdmin bit NOT NULL,
	CreatedDateTime DateTime NULL,
    CONSTRAINT [PK_Users_UserID] PRIMARY KEY CLUSTERED (ID ASC)
)

CREATE TABLE dbo.[Books]
(
    ID INT IDENTITY(1,1) NOT NULL,
	ISBN VARCHAR(60) NOT NULL,
    BookName VARCHAR(40) NOT NULL,
    Price Decimal(7,2),
    Details VARCHAR(255) NULL,
    ImageUrl VARCHAR(150) NULL,
    DownloadUrl VARCHAR(150) NULL,
	CreatedDateTime DateTime NULL,
	UpdatedDateTime DateTime NULL,
    CONSTRAINT [PK_Books_ID] PRIMARY KEY CLUSTERED (ID ASC),
)

--ALTER TABLE dbo.Books
--ALTER COLUMN Price Decimal(7,2);

--DROP Table dbo.Users_Books
CREATE TABLE dbo.[Users_Books]
(
    UserID INT NOT NULL,
	BookID INT NOT NULL,
	CreatedDateTime DateTime NULL,
    CONSTRAINT [FK_Users_UserID] FOREIGN KEY (UserID) REFERENCES Users(ID),
    CONSTRAINT [FK_Books_BookID] FOREIGN KEY (BookID) REFERENCES Books(ID)
)

CREATE UNIQUE NONCLUSTERED INDEX [NCAK_Users_UserID_Books_BookID] ON Users_Books(UserID,BookID);

CREATE TABLE dbo.[Authors]
(
    ID INT IDENTITY(1,1) NOT NULL,
    FirstName VARCHAR(40) NOT NULL,
    LastName VARCHAR(40) NOT NULL,
	CreatedDateTime DateTime NULL,
	UpdatedDateTime DateTime NULL,
    CONSTRAINT [PK_Authors_ID] PRIMARY KEY CLUSTERED (ID ASC),
)
--DROP Table dbo.Books_Authors
CREATE TABLE dbo.[Books_Authors]
(
	BookID INT NOT NULL,
	AuthorID INT NOT NULL,
	CreatedDateTime DateTime NULL,
    CONSTRAINT [FK_Books_Authors_BookID] FOREIGN KEY (BookID) REFERENCES Books(ID),
    CONSTRAINT [FK_Books_Authors_AuthorID] FOREIGN KEY (AuthorID) REFERENCES Authors(ID)
)

CREATE UNIQUE NONCLUSTERED INDEX [NCAK_Books_BookID_Authors_AuthorID] ON Books_Authors(BookID,AuthorID);

--Drop Table dbo.[EventLog]
CREATE TABLE dbo.[EventLog]
(
   ID INT IDENTITY(1,1) NOT NULL,
   EventTime DateTime,
   EventType VARCHAR(50),
   LoginName VARCHAR(50),
   SchemaName VARCHAR(10),
   TableName VARCHAR(50),
   CONSTRAINT [PK_EventLog_ID] PRIMARY KEY CLUSTERED (ID ASC)
);

--DROP TABLE dbo.Audit
CREATE TABLE dbo.[Audit]
(
   ID INT IDENTITY(1,1) NOT NULL,
   UserID INT NOT NULL,
   BookID INT NOT NULL,
   Operation VARCHAR(50),
   NewValue VARCHAR(50),
   OldValue VARCHAR(50),
   CreatedDateTime DateTime
   CONSTRAINT [PK_Audit_ID] PRIMARY KEY CLUSTERED (ID ASC)
);


