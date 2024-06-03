CREATE PROCEDURE [dbo].[spSale_Insert]
	@Id int output,
	@CasherId nvarchar(128),
	@SaleDate datetime2,
	@SubTotal money,
	@Tax money,
	@Total money
AS
begin
	set nocount on;

	insert into dbo.Sale(CasherId, SaleDate, SubTotal, Tax, Total)
	values (@CasherId, @SaleDate, @SubTotal, @Tax, @Total);

	select @Id = SCOPE_IDENTITY();
end
