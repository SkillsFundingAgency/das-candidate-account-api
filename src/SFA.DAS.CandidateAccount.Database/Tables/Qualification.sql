CREATE TABLE dbo.[Qualification] (
    [Id]					    uniqueidentifier	NOT NULL,
    [Subject]  		    	    nvarchar(150)	    NOT NULL,
    [Grade]  			        nvarchar(150)  	    NULL,
    [ToYear]    			    smallint    	    NULL,
    [IsPredicted]               bit                 NOT NULL,
    [ApplicationId]             uniqueidentifier    NOT NULL,
    [QualificationReferenceId]  uniqueidentifier    NOT NULL,
    [AdditionalInformation]     nvarchar(50)        NULL
    CONSTRAINT [PK_Qualification] PRIMARY KEY (Id),
    CONSTRAINT [FK_Qualification_QualificationReference] FOREIGN KEY (QualificationReferenceId) REFERENCES [QualificationReference](Id),
    CONSTRAINT [FK_Qualification_Application] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id),
    INDEX [IX_Qualification_ApplicationId] NONCLUSTERED(ApplicationId)
    )