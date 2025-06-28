-- ========================================
-- DROP SEMUA FOREIGN KEYS
-- ========================================
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += 
    'ALTER TABLE [' + OBJECT_SCHEMA_NAME(parent_object_id) + '].[' + OBJECT_NAME(parent_object_id) + '] DROP CONSTRAINT [' + name + '];'
FROM sys.foreign_keys;
EXEC sp_executesql @sql;

-- ========================================
-- DROP SEMUA TABEL
-- ========================================
EXEC sp_msforeachtable 'DROP TABLE ?';

-- ========================================
-- DROP SEMUA VIEWS
-- ========================================
SET @sql = N'';
SELECT @sql += 
    'DROP VIEW [' + SCHEMA_NAME(schema_id) + '].[' + name + '];'
FROM sys.views;
EXEC sp_executesql @sql;

-- ========================================
-- DROP SEMUA STORED PROCEDURES
-- ========================================
SET @sql = N'';
SELECT @sql += 
    'DROP PROCEDURE [' + SCHEMA_NAME(schema_id) + '].[' + name + '];'
FROM sys.procedures;
EXEC sp_executesql @sql;
