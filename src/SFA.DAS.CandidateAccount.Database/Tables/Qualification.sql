CREATE TABLE dbo.[Qualification] (
    [Id]					uniqueidentifier		NOT NULL,
    [Type]   			    nvarchar(150)	NOT NULL,
    [Subject]  		    	nvarchar(150)	NOT NULL,
    [Grade]  			    nvarchar(150)  	NOT NULL,
    [ToYear]    			smallint    	NOT NULL,
    [IsPredicted]           bit             NOT NULL,
    [ApplicationTemplateId] uniqueidentifier    NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    CONSTRAINT [FK_Template] FOREIGN KEY (ApplicationTemplateId) REFERENCES [Candidate].[Id]
    )