using MongoDataAccess.Models;
using MongoDB.Driver;

namespace MongoDataAccess.DataAccess;

public class ChoreDataAccess
{
    private const string ConnectionString = "mongodb://127.0.0.1:27017";

    private const string DatabaseName = "choredb";

    private const string ChoreCollection = "chore_chart";

    private const string UserCollection = "users";

    private const string ChoreHistoryCollection = "chore_history";

    public IMongoCollection<T> ConnectToMongo<T>(in string collection)
    {
        var client = new MongoClient(ConnectionString);

        var db = client.GetDatabase(DatabaseName);

        return db.GetCollection<T>(collection);
    }

    public async Task<IEnumerable<UserModel>> GetAllUsers()
    {
        var usersCollection = ConnectToMongo<UserModel>(UserCollection);

        var result = await usersCollection.FindAsync(_ => true);

        return result.ToEnumerable();
    }

    public async Task<IEnumerable<ChoreModel>> GetAllChores()
    {
        var choreColletion = ConnectToMongo<ChoreModel>(ChoreCollection);

        var results = await choreColletion.FindAsync(_ => true);

        return results.ToEnumerable();
    }

    public async Task<IEnumerable<ChoreModel>> GetAllChoresForAUser(UserModel user)
    {
        var choreColletion = ConnectToMongo<ChoreModel>(ChoreCollection);

        var results = await choreColletion.FindAsync(c => c.AssignedTo.Id == user.Id);

        return results.ToEnumerable();
    }

    public Task CreateUser(UserModel user)
    {
        var usersCollection = ConnectToMongo<UserModel>(UserCollection);

        return usersCollection.InsertOneAsync(user);
    }

    public Task CreateChore(ChoreModel chore)
    {
        var choreColletion = ConnectToMongo<ChoreModel>(ChoreCollection);

        return choreColletion.InsertOneAsync(chore);
    }

    public Task UpdateChore(ChoreModel chore)
    {
        var choreColletion = ConnectToMongo<ChoreModel>(ChoreCollection);

        var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);

        return choreColletion.ReplaceOneAsync(
            filter: filter,
            replacement: chore,
            options: new ReplaceOptions { IsUpsert = true });
    }

    public Task DeleteChore(ChoreModel chore)
    {
        var choreColletion = ConnectToMongo<ChoreModel>(ChoreCollection);

        return choreColletion.DeleteOneAsync(c => c.Id == chore.Id);
    }

    public async Task CompleteChore(ChoreModel chore)
    {
        var choreCollection = ConnectToMongo<ChoreModel>(ChoreCollection);

        var filter = Builders<ChoreModel>.Filter.Eq("Id", chore.Id);

        await choreCollection.ReplaceOneAsync(filter, chore);

        var choreHistoryCollection = ConnectToMongo<ChoreHistoryModel>(ChoreHistoryCollection);

        await choreHistoryCollection.InsertOneAsync(new ChoreHistoryModel(chore));
    }
}
