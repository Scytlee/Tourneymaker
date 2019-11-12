CREATE PROCEDURE [dbo].[spTournaments_GetAll]
AS
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Tournaments;
RETURN 0
