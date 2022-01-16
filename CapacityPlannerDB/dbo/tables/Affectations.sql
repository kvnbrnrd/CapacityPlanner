CREATE TABLE [dbo].[Affectations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[CollaborateurId] INT NOT NULL,
	[ProjetId] INT NOT NULL,
	[Charge] INT NOT NULL,
	[DateDebut] DATETIME2 NOT NULL,
	[DateFin] DATETIME2 NOT NULL,
	CONSTRAINT [FK_Affectations_Collaborateurs] FOREIGN KEY ([CollaborateurId]) REFERENCES [Collaborateurs]([Id]),
	CONSTRAINT [FK_Affectations_Projets] FOREIGN KEY ([ProjetId]) REFERENCES [Projets]([Id])
)
