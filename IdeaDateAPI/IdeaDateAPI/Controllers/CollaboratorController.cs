using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdeaDateAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdeaDateAPI.Controllers
{
    [Route("api/[controller]")]
    public class CollaboratorController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public CollaboratorController(IUserRepository userRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet("getprojects/{uid}")]
        public IEnumerable<Project> GetProjects(string uid)
        {
            IEnumerable<Project> res = new List<Project>();
            User user = _userRepository.GetUser(uid).Result;
            IEnumerable<Project> projects = _projectRepository.GetProjects().Result;
            return projects.Where(p => !(user.LikedProjects.Contains(p.UID) || user.DismissedProjects.Contains(p.UID)));
        }

        [HttpPost("likeproject")]
        public void LikeProject([FromBody] Dictionary<string, string> value)
        {
            string uid = value["User"];
            User user = _userRepository.GetUser(uid).Result;
            user.LikedProjects.Add(value["Project"]);

            Project p = _projectRepository.GetProject(value["Project"]).Result;
            p.LikedBy.Add(uid);

            _projectRepository.Update(p);
            _userRepository.Update(user);
        }

        [HttpPost("dismissproject")]
        public void DismissProject([FromBody]Dictionary<string, string> value)
        {
            string uid = value["User"];
            User user = _userRepository.GetUser(uid).Result;
            user.DismissedProjects.Add(value["Project"]);
            _userRepository.Update(user);
        }

        [HttpDelete("delete/{uid}")]
        public void DeleteCollaborator(string uid)
        {
            _userRepository.Delete(uid);
        }
    }
}
