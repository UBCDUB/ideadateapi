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

        /// <summary>
        /// Returns list of all Users
        /// </summary>
        /// <returns>All Users</returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            IEnumerable<User> users = _userRepository.GetUsers().Result;
            return users;
        }

        /// <summary>
        /// Gets user by their uid
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>Specified User</returns>
        [HttpGet("{uid}")]
        public User Get(string uid)
        {
            User user = _userRepository.GetUser(uid).Result;
            return user;
        }

        /// <summary>
        /// Creates a new user with specified properties
        /// </summary>
        /// <param name="user"></param>
        /// <returns>UID of created User</returns>
        [HttpPost("create")]
        public ActionResult<String> CreateUser([FromBody] User user)
        {
            string uid = Guid.NewGuid().ToString("N");
            user.UID = uid;
            _userRepository.Add(user);

            return Ok(uid);
        }
    }
}
