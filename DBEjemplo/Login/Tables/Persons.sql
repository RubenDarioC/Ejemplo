CREATE TABLE [Login].[Persons] (
    [PersonId]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (50) NOT NULL,
    [SecoundName]          NVARCHAR (50) NULL,
    [Surname]              NVARCHAR (50) NOT NULL,
    [SecoundSurname]       NVARCHAR (50) NOT NULL,
    [DateOfBirth]          DATETIME      NOT NULL,
    [IdentificationType]   SMALLINT      NOT NULL,
    [IdentificationNumber] INT           NOT NULL,
    CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED ([PersonId] ASC)
);

