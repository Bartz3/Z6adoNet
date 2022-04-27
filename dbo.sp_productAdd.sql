CREATE PROCEDURE [dbo].[sp_productAdd]
@name VARCHAR (50),
@price MONEY,
@categoryId int,
@productID int OUTPUT
AS
INSERT INTO Product (name, price,categoryId) VALUES (@name, @price,@categoryId)
SET @productID = @@IDENTITY