CREATE TABLE dbo.[QualificationReference] (
    [Id]				uniqueidentifier	NOT NULL,
    [Name] 			    nvarchar(500)       NOT NULL,
    [Order] 			tinyint             NOT NULL,
    CONSTRAINT [PK_QualificationReference] PRIMARY KEY (Id)
    )