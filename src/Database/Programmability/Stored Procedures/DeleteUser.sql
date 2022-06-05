CREATE PROCEDURE [dbo].[DeleteUser]
(
	@id BIGINT
)
AS
BEGIN

	DELETE
	FROM [dbo].[User]
	WHERE [Id] = @id;

END