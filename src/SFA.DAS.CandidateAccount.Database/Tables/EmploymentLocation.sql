CREATE TABLE dbo.[EmploymentLocation] (
    [Id]					        uniqueidentifier	NOT NULL,
    [Addresses]   		            nvarchar(500)       NOT NULL,
    [EmployerLocationOption]        tinyint             NOT NULL,
    [EmploymentLocationInformation] nvarchar(max)       NULL,
    [ApplicationId]                 uniqueidentifier    NOT NULL,
    CONSTRAINT [PK_EmploymentLocation] PRIMARY KEY (Id),
    CONSTRAINT [FK_EmploymentLocation_Application] FOREIGN KEY (ApplicationId) REFERENCES [dbo].[Application](Id),
    INDEX [IX_EmploymentLocation_ApplicationId] NONCLUSTERED(ApplicationId)
    )