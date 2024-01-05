CREATE TABLE dbo.[ApprenticeEmailAddressHistory] (
    [Id]					varchar(50)		NOT NULL,
    [EmailAddress]			nvarchar(150)	NOT NULL,
    [ChangedOn]			    datetime		NOT NULL,
    [ApprenticeId]          varchar(50)     NOT NULL
    CONSTRAINT [PK_UserEmailHistory] PRIMARY KEY (Id)
    )