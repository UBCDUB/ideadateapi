﻿using System;
using System.Collections.Generic;
using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes;  
  
namespace IdeaDateAPI.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {

        public string UID { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string GitHub { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<string> TechStack { get; set; }
        public List<string> LikedProjects { get; set; }
        public List<string> DismissedProjects { get; set; }

        public User()
        {
            LikedProjects = new List<string>();
            DismissedProjects = new List<string>();
        }
    }

}