CREATE TABLE [dbo].[Rider] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [Birth]        DATE           NOT NULL,
    [Country]      NVARCHAR (100) NOT NULL,
    [City]         NVARCHAR (100) NULL,
    [Sex]          BIT            NOT NULL,
    [Weight]       FLOAT (53)     NULL,
    [Height]       FLOAT (53)     NULL,
    [Introduction] NVARCHAR (MAX) NULL,
    [Teams]        INT            NULL,
    [Classes]      INT            NULL,
    CONSTRAINT [PK_Rider] PRIMARY KEY CLUSTERED ([Id] ASC)
);

