CREATE TABLE dbo.[WorkExperience] (
    [Id]					uniqueidentifier		NOT NULL,
    [Employer]   			nvarchar(150)	NOT NULL,
    [JobTitle]  			nvarchar(150)	NOT NULL,
    [FromYear]  			smallint      	NOT NULL,
    [ToYear]    			smallint    	NOT NULL,
    [ApplicationTemplateId] uniqueidentifier     NOT NULL,
    [Description]  			nvarchar(max)   NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    CONSTRAINT [FK_Template] FOREIGN KEY (ApplicationTemplateId) REFERENCES [ApplicationTemplate].[Id]
    )