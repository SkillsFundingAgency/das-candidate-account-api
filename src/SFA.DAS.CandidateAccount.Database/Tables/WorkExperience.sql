CREATE TABLE dbo.[WorkExperience] (
    [Id]					uniqueidentifier		NOT NULL,
    [Employer]   			nvarchar(150)	NOT NULL,
    [JobTitle]  			nvarchar(150)	NOT NULL,
    [StartDate]  			datetime      	NOT NULL,
    [EndDate]    			datetime    	NULL,
    [ApplicationId]         uniqueidentifier     NOT NULL,
    [Description]  			nvarchar(max)   NOT NULL
    CONSTRAINT [PK_WorkExperience] PRIMARY KEY (Id),
    CONSTRAINT [FK_WorkExperience_Application] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id)
    )