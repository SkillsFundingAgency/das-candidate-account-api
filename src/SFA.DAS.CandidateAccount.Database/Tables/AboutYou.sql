CREATE TABLE dbo.[AboutYou] (
    [Id]					uniqueidentifier	NOT NULL,
    [Strengths]   			nvarchar(max)	    NOT NULL,
    [Improvements] 			nvarchar(max)       NOT NULL,
    [HobbiesAndInterests]   nvarchar(max)       NOT NULL,
    [Support]  		    	nvarchar(max)       NOT NULL,
    [ApplicationTemplateId] uniqueidentifier    NOT NULL
    CONSTRAINT [PK_AboutYou] PRIMARY KEY (Id),
    CONSTRAINT [FK_AboutYou_ApplicationTemplate] FOREIGN KEY (ApplicationTemplateId) REFERENCES [dbo].[ApplicationTemplate](Id)
    )