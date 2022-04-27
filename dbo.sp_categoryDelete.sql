CREATE PROCEDURE [dbo].[sp_categoryDelete]
@categoryID int OUTPUT
AS
DELETE FROM Category WHERE Id = @categoryID