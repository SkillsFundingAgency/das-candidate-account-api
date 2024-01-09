CREATE TABLE dbo.[ApprenticeEmailAddressHistory] (
    [Id]					uniqueidentifier	NOT NULL,
    [EmailAddress]			nvarchar(150)	    NOT NULL,
    [ChangedOn]			    datetime		    NOT NULL,
    [CandidateId]           uniqueidentifier    NOT NULL
    CONSTRAINT [PK_UserEmailHistory] PRIMARY KEY (Id)
    )