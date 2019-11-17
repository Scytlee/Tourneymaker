CREATE PROCEDURE [dbo].[spMatchups_Update]
	@id INT,
	@WinnerId INT
AS
	SET NOCOUNT ON;

	UPDATE dbo.Matchups
	SET WinnerId = @WinnerId
	WHERE id = @id;
RETURN 0
