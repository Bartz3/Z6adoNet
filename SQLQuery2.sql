CREATE PROCEDURE [dbo].[sp_productUpdate]
@name VARCHAR (50),
@price MONEY,
@productID int
AS
UPDATE Product SET price= @price, name=@name WHERE Id=@productID