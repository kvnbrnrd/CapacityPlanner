﻿CREATE TABLE [dbo].[Projets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Client] NVARCHAR(255) NOT NULL,
	[Nom] NVARCHAR(255) NOT NULL,
	[Statut] NVARCHAR(255) NOT NULL,
	[Type] NVARCHAR(255) NOT NULL,
)
