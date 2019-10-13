using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IdeaDateAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Project
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string GitHubURL { get; set; }
        public string Description { get; set; }
        public string Founder { get; set; }

        /// <summary>
        /// UIDs of users that liked the project
        /// </summary>
        public List<string> LikedBy { get; set; }
        public List<string> TechStack { get; set; }
        public List<string> Collaborators { get; set; }

        public Project()
        {
            LikedBy = new List<string>();
            Collaborators = new List<string>();
        }

    }
}
