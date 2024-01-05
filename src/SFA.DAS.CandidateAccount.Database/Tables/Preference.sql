CREATE TABLE dbo.[Preference] (
    [PreferenceId]			varchar(50)		NOT NULL,
    [PreferenceMeaning]		nvarchar(500)	NOT NULL,
    [PreferenceHint]		nvarchar(500)	NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (PreferenceId)
    )