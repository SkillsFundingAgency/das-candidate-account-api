CREATE TABLE dbo.[Candidate] (
    [Id]					uniqueidentifier	NOT NULL,
    [FirstName]				nvarchar(150)	    NOt NULL,
    [MiddleNames]			nvarchar(150)	    NULL,
    [LastName]				nvarchar(150)	    NOT NULL,
    [Email]					varchar(255)	    NOT NULL,
    [PhoneNumber]			varchar(50)	        NULL,
    [DateOfBirth]			datetime		    NOT NULL,
    [CreatedOn]			    datetime		    NOT NULL,
    [UpdatedOn]			    datetime		    NULL,
    [TermsOfUseAcceptedOn]	datetime		    NULL,
    [GovUkIdentifier]       varchar(150)        NOT NULL
    CONSTRAINT [PK_Candidate] PRIMARY KEY (Id),
    INDEX [IX_Candidate_Email] NONCLUSTERED(Email),
    INDEX [IX_Candidate_GovUkIdentifier] NONCLUSTERED(GovUkIdentifier)
)