using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using IdeaDateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdeaDateAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = _userRepository.GetUsers().Result;
            return users;
        }

        // Get user by UID
        [HttpGet("{uid}")]
        public User Get(string uid)
        {
            User user = _userRepository.GetUser(uid).Result;
            return user;
        }

        // Creates a new user 
        [HttpPost("create")]
        public ActionResult<String> CreateUser([FromBody] User user)
        {
            string uid = Guid.NewGuid().ToString("N");
            user.UID = uid;
            _userRepository.Add(user);

            return Ok(uid);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }


    }
}
