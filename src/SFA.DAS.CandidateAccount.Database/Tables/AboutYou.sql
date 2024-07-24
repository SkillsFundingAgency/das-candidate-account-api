CREATE TABLE dbo.[AboutYou] (
    [Id]					        uniqueidentifier	NOT NULL,
    [Sex]  		    	            tinyint             NULL,
    [IsGenderIdentifySameSexAtBirth]nvarchar(max)       NULL,
    [EthnicGroup]  		            tinyint             NULL,
    [EthnicSubGroup]  		        tinyint             NULL,
    [OtherEthnicSubGroupAnswer]  	nvarchar(max)       NULL,
    [CandidateId]                   uniqueidentifier    NOT NULL
    CONSTRAINT [PK_AboutYou] PRIMARY KEY (Id),
    CONSTRAINT [FK_AboutYou_Candidate] FOREIGN KEY (CandidateId) REFERENCES [dbo].[Candidate](Id),
    INDEX [IX_AboutYou_CandidateId] NONCLUSTERED(CandidateId)
	)