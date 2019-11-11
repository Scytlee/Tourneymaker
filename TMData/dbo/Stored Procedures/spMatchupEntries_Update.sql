CREATE PROCEDURE [dbo].[spMatchupEntries_Update]
	@Id INT,
	@EntryCompetingId INT = NULL,
	@Score FLOAT = NULL
AS
	SET NOCOUNT ON;

	UPDATE dbo.MatchupEntries
	SET EntryCompetingId = @EntryCompetingId, Score = @Score
	WHERE Id = @Id;
RETURN 0
