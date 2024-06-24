CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ProductName] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [QuantityInStock] INT NOT NULL DEFAULT 1,
    [RetailPrice] MONEY NOT NULL,
    [CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [LastModified] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [IsTaxable] BIT NOT NULL DEFAULT 1, 
    [ProductImage] NVARCHAR(500) NULL
)
