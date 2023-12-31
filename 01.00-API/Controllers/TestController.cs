using APIExtension.Const;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;

namespace API.Controllers
{
    [Tags(Actor.Test)]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRepoWrapper repos;
        private readonly IServiceWrapper services;

        public TestController(IRepoWrapper repos, IServiceWrapper services)
        {
            this.repos = repos;
            this.services = services;
        }

        [HttpGet("MonthlyMail")]
        public async Task<IActionResult> MonthlyMail()
        {
            await services.Mails.SendMonthlyStatAsync();
            return Ok();
        }

        [HttpGet("Accounts")]   
        public async Task<IActionResult> Accounts()
        {
            var list =repos.Accounts.GetList();
             return Ok(list);
        }

        [HttpGet("Chats")]
        public async Task<IActionResult> Chats()
        {
            var list = repos.Chats.GetList();
            return Ok(list);
        }

        [HttpGet("Classes")]
        public async Task<IActionResult> Classes()
        {
            var list = repos.Classes.GetList();
            return Ok(list);
        }

        [HttpGet("Connections")]
        public async Task<IActionResult> Connections()
        {
            var list = repos.Connections.GetList()
                .OrderByDescending(e=>e.Start);
            return Ok(list);
        }

        [HttpGet("DocumentFiles")]
        public async Task<IActionResult> DocumentFiles()
        {
            var list = repos.DocumentFiles.GetList();
            return Ok(list);
        }

        [HttpGet("Groups")]
        public async Task<IActionResult> Groups()
        {
            var list = repos.Groups.GetList();
            return Ok(list);
        }

        [HttpGet("GroupMembers")]
        public async Task<IActionResult> GroupMembers()
        {
            var list = repos.GroupMembers.GetList();
            return Ok(list);
        }

        [HttpGet("Invites")]
        public async Task<IActionResult> Invites()
        {
            var list = repos.Invites.GetList();
            return Ok(list);
        }

        [HttpGet("Meetings")]
        public async Task<IActionResult> Meetings()
        {
            var list = repos.Meetings.GetList();
            return Ok(list);
        }

        [HttpGet("Requests")]
        public async Task<IActionResult> Requests()
        {
            var list = repos.Requests.GetList();
            return Ok(list);
        }

        [HttpGet("Reviews")]
        public async Task<IActionResult> Reviews()
        {
            var list = repos.Reviews.GetList();
            return Ok(list);
        }

        [HttpGet("ReviewDetails")]
        public async Task<IActionResult> ReviewDetails()
        {
            var list = repos.ReviewDetails.GetList();
            return Ok(list);
        }

        [HttpGet("Schedules")]
        public async Task<IActionResult> Schedules()
        {
            var list = repos.Schedules.GetList();
            return Ok(list);
        }

        [HttpGet("Subjects")]
        public async Task<IActionResult> Subjects()
        {
            var list = repos.Subjects.GetList();
            return Ok(list);
        }

        [HttpGet("Supervisions")]
        public async Task<IActionResult> Supervisions()
        {
            var list = repos.Supervisions.GetList();
            return Ok(list);
        }
    }
}
