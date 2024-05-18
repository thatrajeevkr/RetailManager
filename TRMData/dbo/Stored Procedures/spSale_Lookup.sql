CREATE PROCEDURE [dbo].[spSale_Lookup]
	@CasherId nvarchar(128),
	@SaleDate datetime2
AS
begin
	set nocount on;

	select Id
	from dbo.Sale
	where CasherId = @CasherId and SaleDate = @SaleDate;
end
