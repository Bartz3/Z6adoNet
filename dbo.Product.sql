CREATE TABLE [dbo].[Product] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [name]       NCHAR (50) NOT NULL,
    [price]      MONEY      NOT NULL,
    [categoryId] INT        NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

