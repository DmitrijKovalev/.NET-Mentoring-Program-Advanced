ALTER TABLE [dbo].[Product]
	ADD CONSTRAINT [FK_Product_Category]
	FOREIGN KEY (CategoryId)
	REFERENCES [Category] (Id)
