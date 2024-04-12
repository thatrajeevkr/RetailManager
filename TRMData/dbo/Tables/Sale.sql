﻿CREATE TABLE [dbo].[Sale]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CasherId] NVARCHAR(128) NOT NULL, 
    [SaleDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [SubTotal] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL, 
    [Total] MONEY NOT NULL, 
    CONSTRAINT [FK_Sale_ToUser] FOREIGN KEY (CasherId) REFERENCES [User](Id)
)
