CREATE PROCEDURE [dbo].[spTournamentEntries_Insert]
	@TournamentId INT,
	@EntryId INT,
	@id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.TournamentEntries (TournamentId, EntryId)
	VALUES (@TournamentId, @EntryId);

	SELECT @id = SCOPE_IDENTITY();
RETURN 0
