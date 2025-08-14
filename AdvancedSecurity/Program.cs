// See https://aka.ms/new-console-template for more information
using AdvancedSecurity;
using AdvancedSecurity.Properties;
using BStorm.Tools.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Numerics;
using System.Text;
using System.Text.Json;
using Tools.Security.Cryptage.Symmetric;

//Le Hash SHA512 est le même que SHA2_512 En Sql
//using (SqlConnection connection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdvancedSecurity;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;"))
//{
//    connection.Open();
//    byte[] hashFromCs = password.Hash();

//    byte[] hashFromSql = (byte[])connection.ExecuteScalar("SELECT HASHBYTES('SHA2_512', @Value)", parameters: new { Value = password })!;

//    Console.WriteLine(Convert.ToBase64String(hashFromCs) == Convert.ToBase64String(hashFromSql));
//}

//AesEncryptor aesEncryptor = new AesEncryptor();
//byte[] key = aesEncryptor.Key;
//byte[] iv = aesEncryptor.IV;

//byte[] cypher = aesEncryptor.Encrypt(password);

//File.WriteAllBytes("passwd.bin", cypher);

//string json = JsonSerializer.Serialize(new { Key = key, IV = iv }, new JsonSerializerOptions() { WriteIndented = true });
//File.WriteAllBytes("KeyInfo.bin", Encoding.Unicode.GetBytes(json));


byte[] KeyInfo = Resources.Keys;
string json = Encoding.Unicode.GetString(KeyInfo);
KeyInfo keyInfo = JsonSerializer.Deserialize<KeyInfo>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })!;

AesEncryptor aesEncryptor2 = new AesEncryptor(keyInfo.Key, keyInfo.IV);
byte[] cypher = Resources.Passwd;
string passwd = aesEncryptor2.Decrypt(cypher);
Console.WriteLine(passwd);

//SymmetricAlgorithm algorithm = null;
//AsymmetricAlgorithm asymmetricAlgorithm = null;


const string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoAdvancedSecurity;User Id={0};Password={1};Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;";

//var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");

//var configuration = builder.Build();
//string connectionString = configuration.GetConnectionString("default")!;

Console.WriteLine(connectionString);

using (SqlConnection connection = new SqlConnection(string.Format(connectionString, Resources.Login, passwd)))
{
    connection.Open();

    var result = connection.ExecuteReader("AppSchema.GetValues", dr => new { Id = (int)dr["Id"], Value = (string)dr["Value"] }, true);

    foreach (var item in result)
    {
        Console.WriteLine(item);
    }
}