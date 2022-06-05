CREATE PROCEDURE [dbo].[DeleteCatalog]
(
	@id	BIGINT
)
AS
BEGIN
	
	DELETE FROM [dbo].[Catalog]
	WHERE [Id] = @id;

END