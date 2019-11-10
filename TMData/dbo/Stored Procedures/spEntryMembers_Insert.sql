CREATE PROCEDURE [dbo].[spEntryMembers_Insert]
	@EntryId INT,
	@PersonId INT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.EntryMembers (EntryId, PersonId)
	VALUES (@EntryId, @PersonId);
RETURN 0
