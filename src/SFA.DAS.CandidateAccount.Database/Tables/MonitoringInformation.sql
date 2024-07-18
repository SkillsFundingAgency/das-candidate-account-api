CREATE TABLE dbo.[MonitoringInformation] (
    [Id]					uniqueidentifier	NOT NULL,
    [Gender]				nvarchar(150)	    NOT NULL,
    [DisabilityStatus]		nvarchar(150)	    NOT NULL,
    [Ethnicity]				nvarchar(150)	    NOT NULL,
    [CandidateId]			uniqueidentifier	NOT NULL
    CONSTRAINT [PK_MonitoringInformation] PRIMARY KEY (Id),
    CONSTRAINT [FK_MonitoringInformation_Candidate] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id),
    INDEX [IX_MonitoringInformation_CandidateId] NONCLUSTERED(CandidateId)
    )