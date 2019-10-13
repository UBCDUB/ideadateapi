﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdeaDateAPI.Models
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Update(User user);
        Task Delete(string id);
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> GetUsers();
    }
}