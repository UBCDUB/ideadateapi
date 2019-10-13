using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdeaDateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IdeaDateAPI.Controllers
{
    [Route("api/[controller]")]
    public class RecruiterController : Controller
    {

        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public RecruiterController(IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        [HttpGet("{uid}")]
        public List<User> GetLikes(string uid)
        {
            List<string> results = new List<string>();
            List<string> likes = (List<string>)
                _projectRepository.GetProject(uid).Result.LikedBy;
            List<User> users = (List<User>) _userRepository.GetUsers().Result
                .Where(x => likes.Contains(x.UID));

            return users;          
        }

        [HttpPost]
        public void LikeUser([FromBody] Dictionary<string, string> jsonBody)
        {
            

        }

        [HttpPost]
        public void DismissUser([FromBody] Dictionary<string, string> jsonBody)
        {

        }

        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Sending with Twilio SendGrid is Fun";
            var to = new EmailAddress("wjwalcher@gmail.com", "Will W");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }


    }
}
