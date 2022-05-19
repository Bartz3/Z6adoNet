CREATE PROCEDURE [dbo].[sp_productAdd]
@name VARCHAR (50),
@price MONEY,
@productID int OUTPUT
AS
INSERT INTO Product (name, price) VALUES (@name, @price)
SET @productID = @@IDENTITY