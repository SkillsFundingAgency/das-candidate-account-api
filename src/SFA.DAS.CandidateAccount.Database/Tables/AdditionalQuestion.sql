CREATE TABLE dbo.[AdditionalQuestion] (
    [Id]					uniqueidentifier	NOT NULL,
    [QuestionText]   		nvarchar(500)       NOT NULL,
    [Answer] 			    nvarchar(max)       NULL,
    [ApplicationId]         uniqueidentifier    NOT NULL
    CONSTRAINT [PK_AdditionalQuestion] PRIMARY KEY (Id),
    CONSTRAINT [FK_AdditionalQuestion_Application] FOREIGN KEY (ApplicationId) REFERENCES [dbo].[Application](Id),
    INDEX [IX_AdditionalQuestion_ApplicationId] NONCLUSTERED(ApplicationId)
    )