CREATE PROCEDURE [dbo].[spMatchupEntries_GetByMatchup]
	@MatchupId INT
AS
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.MatchupEntries
	WHERE MatchupId = @MatchupId;
RETURN 0
