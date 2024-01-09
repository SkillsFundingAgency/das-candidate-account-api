CREATE TABLE dbo.[Education] (
    [Id]					uniqueidentifier	NOT NULL,
    [Institution]  			nvarchar(150)	    NOT NULL,
    [FromYear]  			smallint      	    NOT NULL,
    [ToYear]    			smallint    	    NOT NULL,
    [ApplicationTemplateId] uniqueidentifier    NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    CONSTRAINT [FK_Template] FOREIGN KEY (ApplicationTemplateId) REFERENCES [ApplicationTemplate].[Id]
    )