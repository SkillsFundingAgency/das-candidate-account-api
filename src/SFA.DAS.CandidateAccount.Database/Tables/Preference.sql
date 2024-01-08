CREATE TABLE dbo.[Preference] (
    [PreferenceId]			uniqueidentifier		NOT NULL,
    [PreferenceMeaning]		nvarchar(500)	NOT NULL,
    [PreferenceHint]		nvarchar(500)	NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (PreferenceId)
    )