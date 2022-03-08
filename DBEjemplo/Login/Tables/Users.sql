CREATE TABLE [Login].[Users] (
    [UserId]   INT           IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (50) NOT NULL,
    [Password] NVARCHAR (50) NOT NULL,
    [State]    BIT           NOT NULL,
    [PersonId] INT           NOT NULL,
    [RolId]    SMALLINT      NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_Users_Persons_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [Login].[Persons] ([PersonId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Users_Rols_RolId] FOREIGN KEY ([RolId]) REFERENCES [Login].[Rols] ([RolId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Users_Rols_RolId]
    ON [Login].[Users]([RolId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_FK_Users_Persons_PersonId]
    ON [Login].[Users]([PersonId] ASC);

