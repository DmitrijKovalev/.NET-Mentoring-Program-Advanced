CREATE TABLE [dbo].[Category]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [ImageUrl] NVARCHAR(MAX) NULL, 
    [ParentCategoryId] INT NULL
)
