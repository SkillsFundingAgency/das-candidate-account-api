CREATE TABLE dbo.[ApplicationTemplate] (
    [Id]				uniqueidentifier	NOT NULL,
    [CandidateId]       uniqueidentifier    NOT NULL,
    [DisabilityStatus]  nvarchar(150)       NULL,
    [VacancyReference]  nvarchar(150)       NOT NULL,
    [Status]            tinyint             NOT NULL,
    [CreatedDate]       DateTime            NOT NULL, 
    [UpdatedDate]       DateTime            NULL 
    CONSTRAINT [PK_Template] PRIMARY KEY (Id),
    INDEX [IX_ApplicationTemplate_CandidateIdVacancyReference] NONCLUSTERED(CandidateId, VacancyReference),
    INDEX [IX_ApplicationTemplate_CandidateId] NONCLUSTERED(CandidateId),
    CONSTRAINT [FK_ApplicationTemplate_CandidateId] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id)
)