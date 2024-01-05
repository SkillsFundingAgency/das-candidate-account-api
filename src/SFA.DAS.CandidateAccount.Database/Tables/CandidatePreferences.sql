CREATE TABLE dbo.[CandidatePreferences] (
    [Id]					varchar(50)		NOT NULL,
    [ApprenticeId]			varchar(50)	    NOT NULL,
    [PreferenceId]			varchar(50)	    NOT NULL,
    [Status]				nvarchar(150)	NOT NULL,
    [CreatedOn]			    datetime		NOT NULL,
    [UpdatedOn]			    datetime		NULL

    CONSTRAINT [PK_User] PRIMARY KEY (Id)
    )