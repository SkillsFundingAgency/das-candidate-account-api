CREATE TABLE dbo.[MonitoringInformation] (
    [Id]					uniqueidentifier	NOT NULL,
    [Gender]				nvarchar(150)	    NOT NULL,
    [DisabilityStatus]		nvarchar(150)	    NOT NULL,
    [Ethnicity]				nvarchar(150)	    NOT NULL,
    [CandidateId]			uniqueidentifier	NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    CONSTRAINT [FK_Candidate] FOREIGN KEY (CandidateId) REFERENCES [Candidate].[Id]
    )