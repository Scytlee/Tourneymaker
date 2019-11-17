CREATE PROCEDURE [dbo].[spTournaments_GetById]
	@id INT
AS
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Tournaments
	WHERE id = @id;
RETURN 0
