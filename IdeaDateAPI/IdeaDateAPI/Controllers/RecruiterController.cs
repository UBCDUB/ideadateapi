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
            List<string> likes =
                _projectRepository.GetProject(uid).Result.LikedBy;
            List<User> users = _userRepository.GetUsers().Result
                .Where(x => likes.Contains(x.UID)).ToList();

            return users;          
        }

        [HttpPost("likeuser")]
        public void LikeUser([FromBody] Dictionary<string, string> jsonBody)
        {
            string to_uid = jsonBody["user_uid"];
            Project p = _projectRepository.GetProject(jsonBody["project_uid"]).Result;
            p.Collaborators.Add(to_uid);
            _projectRepository.Update(p);

            User fromUser = _userRepository.GetUser(p.Founder).Result;
            User toUser = _userRepository.GetUser(to_uid).Result;

            SendMail(fromUser, toUser, p).Wait();
        }

        [HttpPost("dismissuser")]
        public void DismissUser([FromBody] Dictionary<string, string> jsonBody)
        {
            string user_uid = jsonBody["user_uid"];
            Project p = _projectRepository.GetProject(jsonBody["project_uid"]).Result;
            p.LikedBy.Remove(user_uid);
            _projectRepository.Update(p);

        }

        static async Task SendMail(User fromUser, User toUser, Project p)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            Console.WriteLine("SG API Key: " + apiKey);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com", "IdeaDate Service");
            var subject = fromUser.Name + " Wants to Collaborate With You!";
            var to = new EmailAddress(toUser.Email, toUser.Name);
            var plainTextContent = "";
            var htmlContent = "Hi " + toUser.Name + "!<br>" +
                fromUser.Name + " wants to collaborate with you on their project, "
                + "<a href=" + p.GitHubURL + "><b>" + p.Name + "</b></a>.<br>"
                + "Get in touch with them via the following info to start working:<br>"
                + "<b>GitHub:</b><a href=https://www.github.com/" + fromUser.GitHub + ">" + fromUser.GitHub + "</a><br>" + "<b>Email:</b> "
                + fromUser.Email + "<br><br>"
                + "Keep on hacking! <br>Sincerely,<br><i>The IdeaDate Team<i>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject,
                plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
            Console.WriteLine("Status code: " + response.StatusCode);
        }


    }
}
