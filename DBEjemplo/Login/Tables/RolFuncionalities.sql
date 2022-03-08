CREATE TABLE [Login].[RolFuncionalities] (
    [Id]          SMALLINT       IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [Controller]  NVARCHAR (100) NOT NULL,
    [State]       BIT            NOT NULL,
    [RolId]       SMALLINT       NULL,
    CONSTRAINT [PK_RolFuncionalities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RolFuncionalities_Rols_RolId] FOREIGN KEY ([RolId]) REFERENCES [Login].[Rols] ([RolId])
);


GO
CREATE NONCLUSTERED INDEX [IX_FK_RolFuncionalities_Rols_RolId]
    ON [Login].[RolFuncionalities]([RolId] ASC);

