CREATE TABLE [dbo].[EntryMembers]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
	[EntryId] INT NOT NULL,
	[PersonId] INT NOT NULL, 
    CONSTRAINT [FK_EntryMembers_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [People]([id]), 
    CONSTRAINT [FK_EntryMembers_EntryId] FOREIGN KEY ([EntryId]) REFERENCES [Entries]([id])
)
