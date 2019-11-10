CREATE PROCEDURE [dbo].[spEntries_GetAll]
AS
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Entries;
RETURN 0
