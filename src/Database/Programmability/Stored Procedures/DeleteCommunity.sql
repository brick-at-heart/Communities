CREATE PROCEDURE [dbo].[DeleteCommunity]
(
	@id	BIGINT
)
AS
BEGIN

	DELETE FROM [dbo].[Community]
	WHERE [Id] = @id;

END