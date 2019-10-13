using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaDateAPI.Models
{
    public interface IProjectRepository
    {
        Task Add(Project project);
        Task Update(Project project);
        Task Delete(string id);
        Task<Project> GetProject(string id);
        Task<IEnumerable<Project>> GetProjects();
    }
}