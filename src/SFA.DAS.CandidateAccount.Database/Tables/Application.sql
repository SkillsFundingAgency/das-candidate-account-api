CREATE TABLE dbo.[Application] (
    [Id]				                uniqueidentifier	NOT NULL,
    [CandidateId]                       uniqueidentifier    NOT NULL,
    [DisabilityStatus]                  nvarchar(150)       NULL,
    [VacancyReference]                  nvarchar(150)       NOT NULL,
    [Status]                            tinyint             NOT NULL default(0),
    [CreatedDate]                       DateTime            NOT NULL default(GETDATE()), 
    [UpdatedDate]                       DateTime            NULL,
    [WithdrawnDate]                     DateTime            NULL,
    [IsEducationHistoryComplete]        tinyint             NOT NULL default(0),
    [IsWorkHistoryComplete]             tinyint             NOT NULL default(0),
    [IsApplicationQuestionsComplete]    tinyint             NOT NULL default(0),
    [IsInterviewAdjustmentsComplete]    tinyint             NOT NULL default(0),
    [IsDisabilityConfidenceComplete]    tinyint             NOT NULL default(0),
    CONSTRAINT [PK_Application] PRIMARY KEY (Id),
    INDEX [IX_Application_CandidateIdVacancyReference] NONCLUSTERED(CandidateId, VacancyReference),
    INDEX [IX_Application_CandidateId] NONCLUSTERED(CandidateId),
    CONSTRAINT [FK_Application_CandidateId] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id)
)