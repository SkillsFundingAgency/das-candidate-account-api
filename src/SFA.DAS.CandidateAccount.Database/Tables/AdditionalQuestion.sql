CREATE TABLE dbo.[AdditionalQuestion] (
    [Id]					uniqueidentifier	NOT NULL,
    [QuestionId]   			uniqueidentifier    NOT NULL,
    [Answer] 			    nvarchar(max)       NOT NULL,
    [ApplicationId]         uniqueidentifier    NOT NULL
    CONSTRAINT [PK_AdditionalQuestion] PRIMARY KEY (Id),
    CONSTRAINT [FK_AdditionalQuestion_Application] FOREIGN KEY (ApplicationId) REFERENCES [dbo].[Application](Id)
    )