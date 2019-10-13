using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdeaDateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace IdeaDateAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet("{id}")]
        public Project Get(string id)
        {
            Project project = _projectRepository.GetProject(id).Result;
            return project;
        }

        [HttpPost("create")]
        public ActionResult<string> Post([FromBody]Project value)
        {
            value.UID = value.GitHubURL;
            _projectRepository.Add(value);
            return Ok(value.UID);
        }

        [HttpPut("edit")]
        public void Put([FromBody]Project value)
        {
            _projectRepository.Update(value);
        }

        [HttpDelete("delete/{id}")]
        public void Delete(string id)
        {
            _projectRepository.Delete(id);
        }
    }
}
