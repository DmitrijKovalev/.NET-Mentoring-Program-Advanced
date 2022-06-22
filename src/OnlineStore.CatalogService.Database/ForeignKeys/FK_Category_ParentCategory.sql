ALTER TABLE [dbo].[Category]
	ADD CONSTRAINT [FK_Category_ParentCategory]
	FOREIGN KEY (ParentCategoryId)
	REFERENCES [Category] (Id)
