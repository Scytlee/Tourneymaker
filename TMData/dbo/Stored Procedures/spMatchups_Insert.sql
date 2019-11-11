CREATE PROCEDURE [dbo].[spMatchups_Insert]
	@TournamentId INT,
	@MatchupRound INT,
	@Id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.Matchups (TournamentId, MatchupRound)
	VALUES (@TournamentId, @MatchupRound);

	SELECT @Id = SCOPE_IDENTITY();
RETURN 0
