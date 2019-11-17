CREATE TABLE [dbo].[TournamentEntries]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TournamentId] INT NOT NULL,
	[EntryId] INT NOT NULL, 
    CONSTRAINT [FK_TournamentEntries_TournamentId] FOREIGN KEY ([TournamentId]) REFERENCES [Tournaments]([id]), 
    CONSTRAINT [FK_TournamentEntries_EntryId] FOREIGN KEY ([EntryId]) REFERENCES [Entries]([id])
)
