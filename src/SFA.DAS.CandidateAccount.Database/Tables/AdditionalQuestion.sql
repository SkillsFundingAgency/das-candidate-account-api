CREATE TABLE dbo.[AdditionalQuestion] (
    [Id]					uniqueidentifier	NOT NULL,
    [QuestionId]   			nvarchar(150)       NOT NULL,
    [Answer] 			    nvarchar(max)       NULL,
    [ApplicationId]         uniqueidentifier    NOT NULL
    CONSTRAINT [PK_AdditionalQuestion] PRIMARY KEY (Id),
    CONSTRAINT [FK_AdditionalQuestion_Application] FOREIGN KEY (ApplicationId) REFERENCES [dbo].[Application](Id)
    )