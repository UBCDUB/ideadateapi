﻿using System;
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

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(string id)
        {
            User user = _userRepository.GetUser(id).Result;
            return user;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
