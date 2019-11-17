CREATE PROCEDURE [dbo].[spTournaments_Update]
	@id INT,
	@CurrentRound INT
AS
	SET NOCOUNT ON;

	UPDATE dbo.Tournaments
	SET CurrentRound = @CurrentRound
	WHERE id = @id;
RETURN 0
