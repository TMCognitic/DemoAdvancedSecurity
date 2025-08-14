// See https://aka.ms/new-console-template for more information
using Tools.Security.Cryptage.Symmetric;

string password = "Test1234=";

AesEncryptor aesEncryptor = new AesEncryptor(SymmetricKeySizes.Size256);


byte[] key = aesEncryptor.Key;
byte[] iv = aesEncryptor.Vector;

byte[] cypher = aesEncryptor.Encrypt(password);


AesEncryptor aesEncryptor2 = new AesEncryptor(key, iv);
Console.WriteLine(aesEncryptor2.Decrypt(cypher));




//SymmetricAlgorithm algorithm = null;
//AsymmetricAlgorithm asymmetricAlgorithm = null;


//const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdvancedSecurity;User Id={0};Password={1};Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;";

//var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

//var configuration = builder.Build();
//string connectionString = configuration.GetConnectionString("default")!;

//Console.WriteLine(connectionString);

//using (SqlConnection connection = new SqlConnection(string.Format(connectionString, Resources.Login, Resources.Password)))
//{
//    connection.Open();

//    var result = connection.ExecuteReader("AppSchema.GetValues", dr => new { Id = (int)dr["Id"], Value = (string)dr["Value"] }, true);

//    foreach(var item in result)
//    {
//        Console.WriteLine(item);
//    }
//}