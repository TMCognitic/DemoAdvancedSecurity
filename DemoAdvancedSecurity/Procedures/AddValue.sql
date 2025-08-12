CREATE PROCEDURE [AppSchema].[AddValue]
	@Value NVARCHAR(50)
AS
BEGIN
	BEGIN TRY
		IF @Value IS NULL OR LEN(TRIM(@Value)) = 0
		BEGIN
			RAISERROR ('@Value is incorrect', 16, 1);
		END

		INSERT INTO Demo ([Value]) VALUES (@Value);
	END TRY
	BEGIN CATCH
		DECLARE @Message NVARCHAR(4000) = ERROR_MESSAGE();
		DECLARE @Severity INT = ERROR_SEVERITY();
		DECLARE @State INT = ERROR_STATE();

		RAISERROR (@Message, @Severity, @State);
	END CATCH
END

