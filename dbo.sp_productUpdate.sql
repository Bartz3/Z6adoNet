CREATE PROCEDURE [dbo].[sp_productUpdate]
@name VARCHAR (50),
@price MONEY,
@productID int,
@categoryId int
AS
UPDATE Product SET price= @price, name=@name,categoryId=@categoryId WHERE Id=@productID