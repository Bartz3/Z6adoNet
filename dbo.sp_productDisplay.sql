CREATE PROCEDURE [dbo].[sp_productDisplay]
AS
SELECT * FROM Product

/*p.id,p.name,p.price,p.categoryId,c.longName,c.Id 
FROM Product p, Category c
WHERE p.categoryId=c.Id*/