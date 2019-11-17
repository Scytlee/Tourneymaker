CREATE PROCEDURE [dbo].[spPeople_Insert]
	@Nickname NVARCHAR(50),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@DiscordTag NVARCHAR(100),
	@EmailAddress NVARCHAR(200),
	@id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.People (Nickname, FirstName, LastName, DiscordTag, EmailAddress)
	VALUES (@Nickname, @FirstName, @LastName, @DiscordTag, @EmailAddress);

	SELECT @id = SCOPE_IDENTITY();
RETURN 0
