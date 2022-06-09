using MongoDataAccess.DataAccess;
using MongoDataAccess.Models;
using MongoDB.Driver;
using MongoDBDemo;

//string connectionString = "mongodb://127.0.0.1:27017";
//string databaseName = "simple_db";
//string collectionName = "people";

//var client = new MongoClient(connectionString);
//var db = client.GetDatabase(databaseName);
//var collection = db.GetCollection<PersonModel>(collectionName);

//var person = new PersonModel { FirstName = "Luiz", LastName = "Felipe" };

//await collection.InsertOneAsync(person);

//var results = await collection.FindAsync(_ => true);

//foreach (var result in results.ToList())
//    Console.WriteLine($"{result.Id}: {result.FirstName} {result.LastName}");

var db = new ChoreDataAccess();

await db.CreateUser(new UserModel() { FirstName = "Luiz", LastName = "Felipe" });

var users = await db.GetAllUsers();

var chore = new ChoreModel()
{
    AssignedTo = users.FirstOrDefault(),
    ChoreText = "Mow the Lawn",
    FrequencyInDays = 7
};

await db.CreateChore(chore);


