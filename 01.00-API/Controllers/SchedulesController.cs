using APIExtension.Const;
using APIExtension.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interface;
using Swashbuckle.AspNetCore.Annotations;
using APIExtension.ClaimsPrinciple;
using ShareResource.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IValidatorWrapper validators;
        private readonly IServiceWrapper services;

        public SchedulesController(IValidatorWrapper validators, IServiceWrapper services)
        {
            this.validators = validators;
            this.services = services;
        }

        //GET: api/Schedules/Group/groupId
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all Schedule meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Group/{groupId}")]
        public async Task<IActionResult> GetSchedulesForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            IQueryable<ScheduleGetDto> mapped = services.Meetings.GetSchedulesForGroup(groupId);
            return Ok(mapped);
        }
    }
}
