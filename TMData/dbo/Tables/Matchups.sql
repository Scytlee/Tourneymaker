CREATE TABLE [dbo].[Matchups]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TournamentId] INT NOT NULL,
	[WinnerId] INT NULL,
	[MatchupRound] INT NOT NULL, 
	CONSTRAINT [FK_Matchups_TournamentId] FOREIGN KEY ([TournamentId]) REFERENCES [Tournaments]([id]),
    CONSTRAINT [FK_Matchups_WinnerId] FOREIGN KEY ([WinnerId]) REFERENCES [Entries]([id])
)
