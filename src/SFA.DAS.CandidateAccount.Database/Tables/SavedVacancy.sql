CREATE TABLE dbo.[SavedVacancy] (
    [Id]					uniqueidentifier	NOT NULL,
    [CandidateId]			uniqueidentifier	NOT NULL,
    [VacancyReference]      nvarchar(150)       NOT NULL,
    [CreatedOn]             datetime            NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_SavedVacancy] PRIMARY KEY (Id),
    INDEX [IX_SavedVacancy_CandidateIdVacancyReference] NONCLUSTERED(CandidateId, VacancyReference),
    INDEX [IX_SavedVacancy_CandidateId] NONCLUSTERED(CandidateId),
    CONSTRAINT [FK_SavedVacancy_CandidateId] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id)
)
