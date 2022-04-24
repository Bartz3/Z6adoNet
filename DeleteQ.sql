DROP PROCEDURE sp_productDelete
GO
CREATE PROCEDURE [dbo].[sp_productDelete]
@productID int OUTPUT
AS
DELETE FROM Product WHERE Id = @productID

