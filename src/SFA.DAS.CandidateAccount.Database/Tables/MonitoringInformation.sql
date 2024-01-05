CREATE TABLE dbo.[MonitoringInformation] (
    [Id]					varchar(50)		NOT NULL,
    [Gender]				nvarchar(150)	NOT NULL,
    [DisabilityStatus]		nvarchar(150)	NOT NULL,
    [Ethnicity]				nvarchar(150)	NOT NULL,
    [ApprenticeId]			varchar(50)	NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    CONSTRAINT [FK_Candidate] FOREIGN KEY (ApprenticeId) REFERENCES [Candidate].[Id]
    )