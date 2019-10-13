using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdeaDateAPI.Models;
using IdeaDateAPI.Util;
using Microsoft.AspNetCore.Mvc;


namespace IdeaDateAPI.Controllers
{
    [Route("api/[controller]")]
    public class CollaboratorController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;

        public CollaboratorController(IUserRepository userRepository,
            IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet("getprojects")]
        public IEnumerable<Project> GetProjects()
        {
            IEnumerable<Project> res = new List<Project>();
            //User user = _userRepository.GetUser(uid).Result;
            IEnumerable<Project> projects = _projectRepository.GetProjects().Result;

            //IEnumerable<Project> sorted = projects.OrderBy(x =>
            //KeywordAnalyzer.ScoreKeywords(x.TechStack, user.TechStack)).Reverse();

            //return projects.Where(p => !(user.LikedProjects.Contains(p.UID) ||
            //user.DismissedProjects.Contains(p.UID)));
            return projects;
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
