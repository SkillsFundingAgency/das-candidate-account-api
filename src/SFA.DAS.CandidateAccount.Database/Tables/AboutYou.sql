CREATE TABLE dbo.[AboutYou] (
    [Id]					varchar(50)		NOT NULL,
    [Strengths]   			nvarchar(max)	NOT NULL,
    [Improvements] 			nvarchar(max)   NOT NULL,
    [HobbiesAndInterests]   nvarchar(max)   NOT NULL,
    [Support]  		    	nvarchar(max)   NOT NULL,
    [ApplicationTemplateId] varchar(50)     NOT NULL
    CONSTRAINT [PK_User] PRIMARY KEY (Id),
    CONSTRAINT [FK_Template] FOREIGN KEY (ApplicationTemplateId) REFERENCES [ApplicationTemplate].[Id]
    )