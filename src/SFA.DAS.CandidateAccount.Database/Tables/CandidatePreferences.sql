CREATE TABLE dbo.[CandidatePreferences] (
    [Id]					uniqueidentifier		NOT NULL,
    [CandidateId]			uniqueidentifier	    NOT NULL,
    [PreferenceId]			uniqueidentifier	    NOT NULL,
    [Status]				nvarchar(150)	NOT NULL,
    [CreatedOn]			    datetime		NOT NULL,
    [UpdatedOn]			    datetime		NULL

    CONSTRAINT [PK_User] PRIMARY KEY (Id)
    )