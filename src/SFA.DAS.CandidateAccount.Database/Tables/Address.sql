CREATE TABLE dbo.[Address] (
    [Id]					uniqueidentifier	NOT NULL,
    [Uprn]                  nvarchar(12)        NULL,
    [AddressLine1]			nvarchar(150)	    NOT NULL,
    [AddressLine2]			nvarchar(150)	    NULL,
    [Town]      			nvarchar(150)	    NOT NULL default(''),
    [County]    			nvarchar(150)	    NULL,
    [Postcode]              nvarchar(50)        NOT NULL,   
    [Latitude]				nvarchar(150)	    NOT NULL,
    [Longitude]		    	nvarchar(150)	    NOT NULL,
    [CandidateId]           uniqueidentifier    NOT NULL
    CONSTRAINT [PK_Address] PRIMARY KEY (Id),
    CONSTRAINT [FK_Candidate] FOREIGN KEY (CandidateId) REFERENCES [Candidate](Id),
    INDEX [IX_Address_CandidateId] NONCLUSTERED(CandidateId)
    )