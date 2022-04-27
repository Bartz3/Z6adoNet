CREATE PROCEDURE [dbo].[sp_categoryUpdate]
	@shortName varchar(30),
	@longName varchar(80),
	@categoryID int
AS
UPDATE Category SET shortName=@shortName, longName=@longName WHERE Id=@categoryID