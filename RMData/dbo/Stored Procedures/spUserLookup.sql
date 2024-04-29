CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
AS
	SET NOCOUNT ON

	SELECT Id, FirstName, LastName, EmailAddress, CreatedDate
	FROM [dbo].[User]
	WHERE Id = @Id
RETURN 0
