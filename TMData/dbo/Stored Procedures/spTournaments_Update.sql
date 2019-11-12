CREATE PROCEDURE [dbo].[spTournaments_Update]
	@Id INT,
	@Active BIT,
	@CurrentRound INT
AS
	SET NOCOUNT ON;

	UPDATE dbo.Tournaments
	SET Active = @Active, CurrentRound = @CurrentRound
	WHERE Id = @Id;
RETURN 0
