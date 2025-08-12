CREATE PROCEDURE [AppSchema].[GetValues]
AS
BEGIN
	SELECT Id, [Value] FROM Demo;
END
