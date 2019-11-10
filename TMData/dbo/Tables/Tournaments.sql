CREATE TABLE [dbo].[Tournaments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TournamentName] NVARCHAR(100) NOT NULL,
	[Active] BIT NOT NULL,
	[CurrentRound] INT NOT NULL 
)
