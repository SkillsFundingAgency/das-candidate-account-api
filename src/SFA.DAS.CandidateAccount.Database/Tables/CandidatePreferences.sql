CREATE TABLE dbo.[CandidatePreferences] (
    [Id]					uniqueidentifier		NOT NULL,
    [CandidateId]			uniqueidentifier	    NOT NULL,
    [PreferenceId]			uniqueidentifier	    NOT NULL,
    [Status]				tinyint             	NULL,
    [CreatedOn]			    datetime		        NOT NULL,
    [UpdatedOn]			    datetime		        NULL,
    [ContactMethod]         nvarchar(50)            NOT NULL

    CONSTRAINT [PK_CandidatePreferences] PRIMARY KEY (Id)
    )