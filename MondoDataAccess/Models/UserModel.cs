using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MondoDataAccess.Models;

public class UserModel
{
    [BsonId] // Identifier
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}
