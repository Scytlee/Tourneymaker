CREATE PROCEDURE [dbo].[spMatchupEntries_Insert]
	@MatchupId INT,
	@ParentMatchupId INT,
	@EntryCompetingId INT,
	@id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.MatchupEntries (MatchupId, ParentMatchupId, EntryCompetingId)
	VALUES (@MatchupId, @ParentMatchupId, @EntryCompetingId);

	SELECT @id = SCOPE_IDENTITY();
RETURN 0
