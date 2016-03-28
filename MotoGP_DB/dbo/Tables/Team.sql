CREATE TABLE [dbo].[Team] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Year]    INT            NOT NULL,
    [Name]    NVARCHAR (200) NOT NULL,
    [Class]   INT            NULL,
    [Bike]    NVARCHAR (200) NULL,
    [cc]      INT            NULL,
    [Company] INT            NULL
);

