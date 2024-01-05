CREATE TABLE dbo.[ApplicationTemplate] (
    [Id]					varchar(50)		NOT NULL,
    [ApprenticeId]          varchar(50)    NOT NULL,
    [DisabilityStatus]      nvarchar(150)   NOT NULL
    CONSTRAINT [PK_Template] PRIMARY KEY (Id)
    )