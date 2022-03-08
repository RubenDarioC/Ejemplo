CREATE TABLE [Login].[Rols] (
    [RolId]       SMALLINT      IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Description] NVARCHAR (50) NOT NULL,
    [State]       BIT           NOT NULL,
    CONSTRAINT [PK_Rols] PRIMARY KEY CLUSTERED ([RolId] ASC)
);

