CREATE TABLE dbo.[Candidate] (
    [Id]					varchar(50)		NOT NULL,
    [FirstName]				nvarchar(150)	NULL,
    [MiddleNames]			nvarchar(150)	NULL,
    [LastName]				nvarchar(150)	NULL,
    [Email]					varchar(255)	NOT NULL,
    [PhoneNumber]			varchar(50)	    NOT NULL,
    [DateOfBirth]			datetime		NOT NULL,
    [CreatedOn]			    datetime		NOT NULL,
    [UpdatedOn]			    datetime		NULL,
    [TermsOfUseAcceptedOn]	datetime		NULL,
    [GovUkIdentifier]       varchar(150)    NULL
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    INDEX [IX_User_Email] NONCLUSTERED(Email),
    INDEX [IX_User_GovUkIdentifier] NONCLUSTERED(GovUkIdentifier)
    )