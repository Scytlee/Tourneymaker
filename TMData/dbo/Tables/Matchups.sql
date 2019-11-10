CREATE TABLE [dbo].[Matchups]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[WinnerId] INT NULL,
	[MatchupRound] INT NOT NULL, 
    CONSTRAINT [FK_Matchups_EntryId] FOREIGN KEY ([WinnerId]) REFERENCES [Entries]([Id])
)
