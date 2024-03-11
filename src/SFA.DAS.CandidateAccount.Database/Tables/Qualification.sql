CREATE TABLE dbo.[Qualification] (
    [Id]					    uniqueidentifier	NOT NULL,
    [Subject]  		    	    nvarchar(150)	    NOT NULL,
    [Grade]  			        nvarchar(150)  	    NOT NULL,
    [ToYear]    			    smallint    	    NOT NULL,
    [IsPredicted]               bit                 NOT NULL,
    [ApplicationId]             uniqueidentifier    NOT NULL,
    [QualificationReferenceId]  uniqueidentifier    NOT NULL
    CONSTRAINT [PK_Qualification] PRIMARY KEY (Id),
    CONSTRAINT [FK_Qualification_QualificationReference] FOREIGN KEY (QualificationReferenceId) REFERENCES [QualificationReference](Id),
    CONSTRAINT [FK_Qualification_Application] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id)
    )