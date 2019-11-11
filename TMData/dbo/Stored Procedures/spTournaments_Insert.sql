﻿CREATE PROCEDURE [dbo].[spTournaments_Insert]
	@TournamentName NVARCHAR(100),
	@Id INT = 0 OUTPUT
AS
	SET NOCOUNT ON;

	INSERT INTO dbo.Tournaments (TournamentName, Active, CurrentRound)
	VALUES (@TournamentName, 1, 1);

	SELECT @Id = SCOPE_IDENTITY();
RETURN 0