CREATE TABLE dbo.[TrainingCourse] (
    [Id]					uniqueidentifier		NOT NULL,
    [Provider]   			nvarchar(150)	NOT NULL,
    [FromYear]  			smallint      	NOT NULL,
    [ToYear]    			smallint    	NOT NULL,
    [ApplicationTemplateId] uniqueidentifier   NOT NULL,
    [Title]  		    	nvarchar(150)   NOT NULL
    CONSTRAINT [PK_Course] PRIMARY KEY (Id),
    CONSTRAINT [FK_Template] FOREIGN KEY (ApplicationTemplateId) REFERENCES [ApplicationTemplate](Id)
    )