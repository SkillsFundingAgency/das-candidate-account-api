CREATE TABLE dbo.[Education] (
    [Id]					uniqueidentifier	NOT NULL,
    [Institution]  			nvarchar(150)	    NOT NULL,
    [FromYear]  			smallint      	    NOT NULL,
    [ToYear]    			smallint    	    NOT NULL,
    [ApplicationId]         uniqueidentifier    NOT NULL,
    CONSTRAINT [PK_Education] PRIMARY KEY (Id),
    CONSTRAINT [FK_Education_Application] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id)
    )