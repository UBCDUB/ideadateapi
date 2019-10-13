using MongoDB.Driver;

namespace IdeaDateAPI.Models
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _mongoDb;
        public MongoDbContext()
        {
            MongoClient client = new MongoClient("mongodb+srv://admin-user:admin-user@ideadate-aicxi.azure.mongodb.net/test?retryWrites=true&w=majority");
            _mongoDb = client.GetDatabase("IdeaDateDb");
        }
        public IMongoCollection<User> Users
        {
            get
            {
                return _mongoDb.GetCollection<User>("Users");
            }
        }
    }
}