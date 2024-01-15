CREATE TABLE dbo.[Application] (
    [Id]				                uniqueidentifier	NOT NULL,
    [CandidateId]                       uniqueidentifier    NOT NULL,
    [DisabilityStatus]                  nvarchar(150)       NULL,
    [VacancyReference]                  nvarchar(150)       NOT NULL,
    [Status]                            tinyint             NOT NULL,
    [CreatedDate]                       DateTime            NOT NULL, 
    [UpdatedDate]                       DateTime            NULL,
    [WithdrawnDate]                     DateTime            NULL,
    [IsEducationHistoryComplete]        tinyint             NOT NULL,
    [IsWorkHistoryComplete]             tinyint             NOT NULL,
    [IsApplicationQuestionsComplete]    tinyint             NOT NULL,
    [IsInterviewAdjustmentsComplete]    tinyint             NOT NULL,
    [IsDisabilityConfidenceComplete]    tinyint             NOT NULL,
    CONSTRAINT [PK_Application] PRIMARY KEY (Id),
    INDEX [IX_Application_CandidateIdVacancyReference] NONCLUSTERED(CandidateId, VacancyReference),
    INDEX [IX_Application_CandidateId] NONCLUSTERED(CandidateId),
    CONSTRAINT [FK_Application_CandidateId] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id)
)