CREATE TABLE dbo.[Application] (
    [Id]				                    uniqueidentifier	NOT NULL,
    [CandidateId]                           uniqueidentifier    NOT NULL,
    [DisabilityStatus]                      nvarchar(150)       NULL,
    [VacancyReference]                      nvarchar(150)       NOT NULL,
    [Status]                                tinyint             NOT NULL default(0),
    [CreatedDate]                           DateTime            NOT NULL default(GETDATE()), 
    [UpdatedDate]                           DateTime            NULL,
    [SubmittedDate]                         DateTime            NULL,
    [WithdrawnDate]                         DateTime            NULL,
    [QualificationsStatus]                  tinyint             NOT NULL default(0),
    [TrainingCoursesStatus]                 tinyint             NOT NULL default(0),
    [JobsStatus]                            tinyint             NOT NULL default(0),
    [WorkExperienceStatus]                  tinyint             NOT NULL default(0),
    [SkillsAndStrengthStatus]               tinyint             NOT NULL default(0),
    [InterestsStatus]                       tinyint             NOT NULL default(0),
    [AdditionalQuestion1Status]             tinyint             NOT NULL default(0),
    [AdditionalQuestion2Status]             tinyint             NOT NULL default(0),
    [InterviewAdjustmentsStatus]            tinyint             NOT NULL default(0),
    [DisabilityConfidenceStatus]            tinyint             NOT NULL default(0),
    [WhatIsYourInterest]                    nvarchar(max)       NULL,
    [ApplyUnderDisabilityConfidentScheme]   bit                 NULL,
    [PreviousAnswersSourceId]               uniqueidentifier    NULL
    CONSTRAINT [PK_Application] PRIMARY KEY (Id),
    INDEX [IX_Application_CandidateIdVacancyReference] NONCLUSTERED(CandidateId, VacancyReference),
    INDEX [IX_Application_CandidateId] NONCLUSTERED(CandidateId),
    CONSTRAINT [FK_Application_CandidateId] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id),
    CONSTRAINT [FK_Application_PreviousAnswersSourceId] FOREIGN KEY (PreviousAnswersSourceId) REFERENCES [Application](Id)
)