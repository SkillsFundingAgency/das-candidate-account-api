CREATE TABLE dbo.[Geopoint] (
    [Id]					uniqueidentifier	NOT NULL,
    [Latitude]				nvarchar(150)	    NOT NULL,
    [Longitude]		    	nvarchar(150)	    NOT NULL,
    [Easting]				nvarchar(150)	    NOT NULL,
    [Northing]				nvarchar(150)	    NOT NULL,
    [AddressId]             uniqueidentifier    NOT NULL
    CONSTRAINT [PK_Location] PRIMARY KEY (Id),    
    CONSTRAINT [FK_Address] FOREIGN KEY (AddressId) REFERENCES [Address].[Id]
    )