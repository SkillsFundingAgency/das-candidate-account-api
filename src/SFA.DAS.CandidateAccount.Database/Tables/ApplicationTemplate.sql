CREATE TABLE dbo.[ApplicationTemplate] (
    [Id]				uniqueidentifier	NOT NULL,
    [CandidateId]       uniqueidentifier    NOT NULL,
    [DisabilityStatus]  nvarchar(150)       NOT NULL
    CONSTRAINT [PK_Template] PRIMARY KEY (Id)
    )