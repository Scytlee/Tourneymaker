CREATE PROCEDURE [dbo].[spTournaments_Insert]
	@TournamentName NVARCHAR(100),
	@id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.Tournaments (TournamentName, CurrentRound)
	VALUES (@TournamentName, 0);

	SELECT @id = SCOPE_IDENTITY();
RETURN 0
