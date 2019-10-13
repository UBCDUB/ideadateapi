using IdeaDateAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaDateAPI.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDbContext _context;

        public UserRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            try
            {
                await _context.Users.InsertOneAsync(user);
            }
            catch
            {
                throw;
            }
        }
        public async Task<User> GetUser(string id)
        {
            try
            {
                FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
                return await _context.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<User>> GetUsers()
        {
            try
            {
                return await _context.Users.Find(_ => true).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Update(User user)
        {
            try
            {
                await _context.Users.ReplaceOneAsync(filter: g => g.UID == user.UID, replacement: user);
            }
            catch
            {
                throw;
            }
        }
        public async Task Delete(string id)
        {
            try
            {
                FilterDefinition<User> data = Builders<User>.Filter.Eq("Id", id);
                await _context.Users.DeleteOneAsync(data);
            }
            catch
            {
                throw;
            }
        }
    }
}