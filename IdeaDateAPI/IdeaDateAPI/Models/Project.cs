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

        /// <summary>
        /// UIDs of users that liked the project
        /// </summary>
        public IEnumerable<string> LikedBy { get; set; }
        public IEnumerable<string> TechStack { get; set; }
    }
}
