CREATE TABLE dbo.[Preference] (
    [PreferenceId]			uniqueidentifier		NOT NULL,
    [PreferenceMeaning]		nvarchar(500)	NOT NULL,
    [PreferenceHint]		nvarchar(500)	NOT NULL,
    [PreferenceType]            nvarchar(50)    NULL
    CONSTRAINT [PK_Preference] PRIMARY KEY (PreferenceId)
    )