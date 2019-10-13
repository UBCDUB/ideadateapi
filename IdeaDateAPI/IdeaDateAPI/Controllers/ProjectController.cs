using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdeaDateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET api/values/5
        [HttpGet("{id}")]
        public Project Get(string id)
        {
            Project project = _projectRepository.GetProject(id).Result;
            return project;
        }

        // POST api/values
        [HttpPost("create")]
        public ActionResult<string> Post([FromBody]Project value)
        {
            string uid = Guid.NewGuid().ToString("N");
            value.UID = uid;
            _projectRepository.Add(value);
            return Ok(value.UID);
        }

        // PUT api/values/5
        [HttpPut("edit")]
        public void Put([FromBody]Project value)
        {
            _projectRepository.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("delete/{id}")]
        public void Delete(string id)
        {
            _projectRepository.Delete(id);
        }
    }
}
