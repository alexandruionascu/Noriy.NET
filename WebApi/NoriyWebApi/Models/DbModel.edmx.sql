
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/20/2014 21:35:04
-- Generated from EDMX file: C:\Users\user\documents\visual studio 2013\Projects\NoriyWebApi\NoriyWebApi\Models\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Noriy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CategoryCategoryLink]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CategoryLinks] DROP CONSTRAINT [FK_CategoryCategoryLink];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Blackurls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Blackurls];
GO
IF OBJECT_ID(N'[dbo].[UrlBlacklists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UrlBlacklists];
GO
IF OBJECT_ID(N'[dbo].[Friendships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Friendships];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[FacebookIds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FacebookIds];
GO
IF OBJECT_ID(N'[dbo].[CategoryLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CategoryLinks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Traffic'
CREATE TABLE [dbo].[Traffic] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [Time] nvarchar(max)  NULL
);
GO

-- Creating table 'UrlBlacklists'
CREATE TABLE [dbo].[UrlBlacklists] (
    [Id] uniqueidentifier  NOT NULL,
    [Url] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Friendships'
CREATE TABLE [dbo].[Friendships] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId1] nvarchar(max)  NOT NULL,
    [UserId2] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FacebookIds'
CREATE TABLE [dbo].[FacebookIds] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [FbId] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CategoryLinks'
CREATE TABLE [dbo].[CategoryLinks] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [CategoryId] int  NOT NULL
);
GO

-- Creating table 'Blackurls'
CREATE TABLE [dbo].[Blackurls] (
    [Id] uniqueidentifier  NOT NULL,
    [UserId] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Traffic'
ALTER TABLE [dbo].[Traffic]
ADD CONSTRAINT [PK_Traffic]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UrlBlacklists'
ALTER TABLE [dbo].[UrlBlacklists]
ADD CONSTRAINT [PK_UrlBlacklists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Friendships'
ALTER TABLE [dbo].[Friendships]
ADD CONSTRAINT [PK_Friendships]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FacebookIds'
ALTER TABLE [dbo].[FacebookIds]
ADD CONSTRAINT [PK_FacebookIds]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CategoryLinks'
ALTER TABLE [dbo].[CategoryLinks]
ADD CONSTRAINT [PK_CategoryLinks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Blackurls'
ALTER TABLE [dbo].[Blackurls]
ADD CONSTRAINT [PK_Blackurls]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryId] in table 'CategoryLinks'
ALTER TABLE [dbo].[CategoryLinks]
ADD CONSTRAINT [FK_CategoryCategoryLink]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategoryLink'
CREATE INDEX [IX_FK_CategoryCategoryLink]
ON [dbo].[CategoryLinks]
    ([CategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------