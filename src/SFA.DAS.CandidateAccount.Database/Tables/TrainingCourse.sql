CREATE TABLE dbo.[TrainingCourse] (
    [Id]					uniqueidentifier		NOT NULL,
    [Provider]   			nvarchar(150)	NULL,
    [FromYear]  			smallint      	NULL,
    [ToYear]    			smallint    	NOT NULL,
    [ApplicationId] uniqueidentifier   NOT NULL,
    [Title]  		    	nvarchar(500)   NOT NULL
    CONSTRAINT [PK_Course] PRIMARY KEY (Id),
    CONSTRAINT [FK_TrainingCourseApplication] FOREIGN KEY (ApplicationId) REFERENCES [Application](Id)
    )