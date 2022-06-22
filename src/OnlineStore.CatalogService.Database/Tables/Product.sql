CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [ImageUrl] NVARCHAR(MAX) NULL, 
    [Price] DECIMAL NOT NULL, 
    [Amount] INT NOT NULL, 
    [CategoryId] INT NOT NULL
)
