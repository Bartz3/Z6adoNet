CREATE PROCEDURE [dbo].[sp_CategoryAdd]
@shortName VARCHAR (30),
@longName VARCHAR (80),
@categoryID int OUTPUT
AS
INSERT INTO Category (shortName,longName) VALUES (@shortName, @longName)
SET @categoryID = @@IDENTITY