CREATE TABLE [dbo].[SaleDetail]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [SaleId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [PurchasePrice] MONEY NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 1,
    [Tax] MONEY NOT NULL DEFAULT 0
)
