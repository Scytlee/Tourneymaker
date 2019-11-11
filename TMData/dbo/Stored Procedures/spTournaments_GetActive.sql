CREATE PROCEDURE [dbo].[spTournaments_GetActive]
AS
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Tournaments
	WHERE Active = 1;
RETURN 0
