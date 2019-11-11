CREATE PROCEDURE [dbo].[spMatchups_Update]
	@Id INT,
	@WinnerId INT
AS
	SET NOCOUNT ON;

	UPDATE dbo.Matchups
	SET WinnerId = @WinnerId
	WHERE Id = @Id;
RETURN 0
