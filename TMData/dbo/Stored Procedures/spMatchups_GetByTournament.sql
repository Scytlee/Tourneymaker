CREATE PROCEDURE [dbo].[spMatchups_GetByTournament]
	@TournamentId INT
AS
	SET NOCOUNT ON;

	SELECT m.*
	FROM dbo.Matchups m
	WHERE m.TournamentId = @TournamentId;
RETURN 0
