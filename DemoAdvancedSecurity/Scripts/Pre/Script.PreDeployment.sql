/*
 Modèle de script de pré-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront exécutées avant le script de compilation.	
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de pré-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de pré-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

IF NOT EXISTS (SELECT * FROM sys.syslogins WHERE [name] = N'AppLogin')
BEGIN
    CREATE LOGIN [AppLogin] WITH PASSWORD=N'Test1234=';
END
