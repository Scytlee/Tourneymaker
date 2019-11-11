CREATE PROCEDURE [dbo].[spTournamentEntries_Insert]
	@TournamentId INT,
	@EntryId INT,
	@Id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.TournamentEntries (TournamentId, EntryId)
	VALUES (@TournamentId, @EntryId);

	SELECT @Id = SCOPE_IDENTITY();
RETURN 0
