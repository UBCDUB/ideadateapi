using MongoDB.Driver;

namespace IdeaDateAPI.Models
{
    public interface IMongoDbContext
    {
        IMongoCollection<User> Users { get; }
    }
}