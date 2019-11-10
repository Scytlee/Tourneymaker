CREATE TABLE [dbo].[TournamentEntries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TournamentId] INT NOT NULL,
	[EntryId] INT NOT NULL, 
    CONSTRAINT [FK_TournamentEntries_TournamentId] FOREIGN KEY ([TournamentId]) REFERENCES [Tournaments]([Id]), 
    CONSTRAINT [FK_TournamentEntries_EntryId] FOREIGN KEY ([EntryId]) REFERENCES [Entries]([Id])
)
