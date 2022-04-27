CREATE TABLE [dbo].[Category] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [shortName] NCHAR (30) NOT NULL,
    [longName]  NCHAR (80) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

