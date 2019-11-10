CREATE PROCEDURE [dbo].[spEntries_GetByTournament]
	@TournamentId INT
AS
	SET NOCOUNT ON;

	SELECT e.*
	FROM dbo.Entries e
	INNER JOIN dbo.TournamentEntries te ON e.Id = te.EntryId
	WHERE te.TournamentId = @TournamentId;
RETURN 0
