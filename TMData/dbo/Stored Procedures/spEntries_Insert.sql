CREATE PROCEDURE [dbo].[spEntries_Insert]
	@EntryName NVARCHAR(100),
	@Id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.Entries (EntryName)
	VALUES (@EntryName);

	SELECT @Id = SCOPE_IDENTITY();
RETURN 0
