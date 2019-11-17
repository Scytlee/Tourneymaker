CREATE PROCEDURE [dbo].[spMatchupEntries_Update]
	@id INT,
	@EntryCompetingId INT = NULL,
	@Score FLOAT = NULL
AS
	SET NOCOUNT ON;

	UPDATE dbo.MatchupEntries
	SET EntryCompetingId = @EntryCompetingId, Score = @Score
	WHERE id = @id;
RETURN 0
