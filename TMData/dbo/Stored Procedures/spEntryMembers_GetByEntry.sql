CREATE PROCEDURE [dbo].[spEntryMembers_GetByEntry]
	@EntryId INT
AS
	SET NOCOUNT ON;

	SELECT p.*
	FROM dbo.EntryMembers em
	INNER JOIN dbo.People p ON em.PersonId = p.Id
	WHERE em.EntryId = @EntryId;
RETURN 0
