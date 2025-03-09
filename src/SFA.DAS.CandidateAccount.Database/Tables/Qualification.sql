CREATE TABLE dbo.[Qualification] (
    [Id]					    uniqueidentifier	NOT NULL,
    [Subject]  		    	    nvarchar(150)	    NOT NULL,
    [Grade]  			        nvarchar(150)  	    NULL,
    [ToYear]    			    smallint    	    NULL,
    [IsPredicted]               bit                 NOT NULL,
    [ApplicationId]             uniqueidentifier    NOT NULL,
    [QualificationOrder]        tinyint             NULL,
    [QualificationReferenceId]  uniqueidentifier    NOT NULL,
    [AdditionalInformation]     nvarchar(max)       NULL,
    [CreatedDate]               DateTime            NULL default(GETDATE()),
    CONSTRAINT [PK_Qualification] PRIMARY KEY (Id),
    CONSTRAINT [FK_Qualification_QualificationReference] FOREIGN KEY (QualificationReferenceId) REFERENCES [QualificationReference](Id),
    CONSTRAINT [FK_Qualification_Application] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id),
    INDEX [IX_Qualification_ApplicationId] NONCLUSTERED(ApplicationId),
    INDEX [IX_Qualification_ApplicationQualificationRefId] NONCLUSTERED(ApplicationId, QualificationReferenceId)
)