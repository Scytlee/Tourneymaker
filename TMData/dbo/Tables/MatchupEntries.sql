CREATE TABLE [dbo].[MatchupEntries]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[MatchupId] INT NOT NULL,
	[ParentMatchupId] INT NULL,
	[EntryCompetingId] INT NULL,
	[Score] FLOAT NULL,
    CONSTRAINT [FK_MatchupEntries_MatchupId] FOREIGN KEY ([MatchupId]) REFERENCES [Matchups]([id]), 
    CONSTRAINT [FK_MatchupEntries_ParentMatchupId] FOREIGN KEY ([ParentMatchupId]) REFERENCES [Matchups]([id]), 
    CONSTRAINT [FK_MatchupEntries_EntryCompetingId] FOREIGN KEY ([EntryCompetingId]) REFERENCES [Entries]([id])
)
