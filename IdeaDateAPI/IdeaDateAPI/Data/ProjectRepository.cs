using IdeaDateAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaDateAPI.Models
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IMongoDbContext _context;

        public ProjectRepository(IMongoDbContext context)
        {
            _context = context;
        }

        public async Task Add(Project project)
        {
            try
            {
                await _context.Projects.InsertOneAsync(project);
            }
            catch
            {
                throw;
            }
        }
        public async Task<Project> GetProject(string id)
        {
            try
            {
                FilterDefinition<Project> filter = Builders<Project>.Filter.Eq("UID", id);
                return await _context.Projects.Find(filter).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Project>> GetProjects()
        {
            try
            {
                return await _context.Projects.Find(_ => true).ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task Update(Project project)
        {
            try
            {
                await _context.Projects.ReplaceOneAsync(filter: g => g.UID == project.UID, replacement: project);
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
                FilterDefinition<Project> data = Builders<Project>.Filter.Eq("UID", id);
                await _context.Projects.DeleteOneAsync(data);
            }
            catch
            {
                throw;
            }
        }
    }
}