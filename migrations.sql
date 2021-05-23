IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Tag] (
    [Id] int NOT NULL IDENTITY,
    [name] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Article] (
    [Id] int NOT NULL IDENTITY,
    [title] nvarchar(50) NOT NULL,
    [description] nvarchar(50) NULL,
    [TagId] int NULL,
    CONSTRAINT [PK_Article] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Article_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Article_TagId] ON [Article] ([TagId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210523054431_V0', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Article].[title]', N'Title', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210523060908_V1', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Article] ADD [Content] ntext NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210523061312_V2', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ArticleTags] (
    [Id] int NOT NULL IDENTITY,
    [TagId] int NOT NULL,
    [ArticleId] int NOT NULL,
    CONSTRAINT [PK_ArticleTags] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ArticleTags_Article_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [Article] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ArticleTags_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_ArticleTags_ArticleId_TagId] ON [ArticleTags] ([ArticleId], [TagId]);
GO

CREATE INDEX [IX_ArticleTags_TagId] ON [ArticleTags] ([TagId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210523063023_V3', N'5.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [ArticleTags] DROP CONSTRAINT [FK_ArticleTags_Article_ArticleId];
GO

ALTER TABLE [ArticleTags] DROP CONSTRAINT [FK_ArticleTags_Tag_TagId];
GO

DROP INDEX [IX_ArticleTags_ArticleId_TagId] ON [ArticleTags];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ArticleTags]') AND [c].[name] = N'TagId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ArticleTags] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [ArticleTags] ALTER COLUMN [TagId] int NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ArticleTags]') AND [c].[name] = N'ArticleId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ArticleTags] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [ArticleTags] ALTER COLUMN [ArticleId] int NULL;
GO

CREATE UNIQUE INDEX [IX_ArticleTags_ArticleId_TagId] ON [ArticleTags] ([ArticleId], [TagId]) WHERE [ArticleId] IS NOT NULL AND [TagId] IS NOT NULL;
GO

ALTER TABLE [ArticleTags] ADD CONSTRAINT [FK_ArticleTags_Article_ArticleId] FOREIGN KEY ([ArticleId]) REFERENCES [Article] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [ArticleTags] ADD CONSTRAINT [FK_ArticleTags_Tag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Tag] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210523063149_V4', N'5.0.6');
GO

COMMIT;
GO

