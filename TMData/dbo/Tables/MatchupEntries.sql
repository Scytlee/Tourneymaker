CREATE TABLE [dbo].[MatchupEntries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[MatchupId] INT NOT NULL,
	[ParentMatchupId] INT NULL,
	[EntryCompetingId] INT NOT NULL,
	[Score] FLOAT NULL, 
    CONSTRAINT [FK_MatchupEntries_MatchupId] FOREIGN KEY ([MatchupId]) REFERENCES [Matchups]([Id]), 
    CONSTRAINT [FK_MatchupEntries_ParentMatchupId] FOREIGN KEY ([MatchupId]) REFERENCES [Matchups]([Id]), 
    CONSTRAINT [FK_MatchupEntries_EntryCompetingId] FOREIGN KEY ([EntryCompetingId]) REFERENCES [Entries]([Id])
)
