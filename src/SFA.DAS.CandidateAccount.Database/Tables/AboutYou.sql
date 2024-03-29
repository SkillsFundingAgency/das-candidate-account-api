CREATE TABLE dbo.[AboutYou] (
    [Id]					uniqueidentifier	NOT NULL,
    [Strengths]   			nvarchar(max)	    NULL,
    [Improvements] 			nvarchar(max)       NULL,
    [HobbiesAndInterests]   nvarchar(max)       NULL,
    [Support]  		    	nvarchar(max)       NULL,
    [ApplicationId]         uniqueidentifier    NOT NULL
    CONSTRAINT [PK_AboutYou] PRIMARY KEY (Id),
    CONSTRAINT [FK_AboutYou_Application] FOREIGN KEY (ApplicationId) REFERENCES [dbo].[Application](Id)
    )