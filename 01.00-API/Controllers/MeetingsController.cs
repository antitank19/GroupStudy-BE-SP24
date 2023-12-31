using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;
using ServiceLayer.Interface;
using APIExtension.Const;
using Swashbuckle.AspNetCore.Annotations;
using APIExtension.ClaimsPrinciple;
using Microsoft.AspNetCore.Authorization;
using ShareResource.DTO;
using APIExtension.Validator;
using AutoMapper;
using System.Collections;
using DataLayer.DBContext;
using AutoMapper.QueryableExtensions;
using API.SignalRHub;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IValidatorWrapper validators;
        private readonly IMapper mapper;
        private readonly GroupStudyContext context;
        private readonly IHubContext<GroupHub> groupHub;

        public MeetingsController(IServiceWrapper services, IValidatorWrapper validators, IMapper mapper, GroupStudyContext context, IHubContext<GroupHub> groupHub)
        {
            this.services = services;
            this.validators = validators;
            this.mapper = mapper;
            this.context = context;
            this.groupHub = groupHub;
        }

        //GET: api/Meetings/Past/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Past/Group/{groupId}")]
        public async Task<IActionResult> GetPastMeetingForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            var mapped = services.Meetings.GetPastMeetingsForGroup(groupId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Schedule/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all Schedule meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Schedule/Group/{groupId}")]
        public async Task<IActionResult> GetScheduleMeetingForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            IQueryable<Meeting> list = services.Meetings.GetScheduleMeetingsForGroup(groupId);
            if (await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId))
            {
                IQueryable<ScheduleMeetingForLeaderGetDto> mapped = list.ProjectTo<ScheduleMeetingForLeaderGetDto>(mapper.ConfigurationProvider);
                return Ok(mapped);
            }
            else
            {
                //return Ok(list);
                IQueryable<ScheduleMeetingGetDto> mapped = list.ProjectTo<ScheduleMeetingGetDto>(mapper.ConfigurationProvider);
                return Ok(mapped);
            }
        }

        //GET: api/Meetings/Schedule/Group/id
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all Live meetings of group"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Live/Group/{groupId}")]
        public async Task<IActionResult> GetLiveMeetingForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            var mapped = services.Meetings.GetLiveMeetingsForGroup(groupId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Past/Student
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of student"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("All/Student")]
        public async Task<IActionResult> GetAllMeetingForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            var mappedPast = services.Meetings.GetPastMeetingsForStudent(studentId);
            var mappedLive = services.Meetings.GetLiveMeetingsForStudent(studentId);
            var mappedSchedule = services.Meetings.GetScheduleMeetingsForStudent(studentId);

            return Ok(new { Past = mappedPast, Live = mappedLive, Schedule = mappedSchedule}); 
        }
        //GET: api/Meetings/Past/Student
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of student"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Past/Student")]
        public async Task<IActionResult> GetPastMeetingForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            var mapped = services.Meetings.GetPastMeetingsForStudent(studentId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Past/Student/month
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all past meetings of group"
            ,Description = "month (yyyy-mm-dd): chỉ cần năm với tháng, day nhập đại"

        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Past/Student/{month}")]
        public async Task<IActionResult> GetPastMeetingForStudentByMonth(DateTime month)
        {
            int studentId = HttpContext.User.GetUserId();
            var mapped = services.Meetings.GetPastMeetingsForStudentByMonth(studentId,month);
            return Ok(mapped);
        }


        //GET: api/Meetings/Schedule/Student
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of student"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Schedule/Student")]
        public async Task<IActionResult> GetScheduleMeetingForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            var mapped = services.Meetings.GetScheduleMeetingsForStudent(studentId);
            return Ok(mapped);
        }

        //GET: api/Meetings/Schedule/Student/{date}
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of student"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Schedule/Student/{date}")]
        public async Task<IActionResult> GetScheduleMeetingForStudent(DateTime date)
        {
            int studentId = HttpContext.User.GetUserId();
            var mapped = services.Meetings.GetScheduleMeetingsForStudentByDate(studentId, date);
            return Ok(mapped);
        }

        //GET: api/Meetings/Live/Student
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all past meetings of student"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Live/Student")]
        public async Task<IActionResult> GetLiveMeetingForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            var mapped = services.Meetings.GetLiveMeetingsForStudent(studentId);
            return Ok(mapped);
        }

        [SwaggerOperation(
          Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Create a new instant meeting"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Instant")]
        public async Task<IActionResult> CreateInstantMeeting(InstantMeetingCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoining = await services.Groups.IsStudentJoiningGroupAsync(studentId, dto.GroupId);
            if (!isJoining)
            {
                return Unauthorized("Bạn không phải thành viên của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            Meeting created= await services.Meetings.CreateInstantMeetingAsync(dto);
            LiveMeetingGetDto mappedCreated = mapper.Map<LiveMeetingGetDto>(created);
            await groupHub.Clients.Group(dto.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg, $"{HttpContext.User.GetUsername()} bắt đầu cuộc họp {dto.Name}");
            return Ok(mappedCreated);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Mass create many schedule meetings within a range of time",
            Description = "ScheduleSRangeStart: chỉ cần date, time ko quan trọng nhưng vẫn phải điền (cho 00:00:00)<br/>" +
                "chủ nhật là 1, thứ 2-7 là 2-7 "
        )]
        //[Authorize(Roles = Actor.Student)]
        [HttpPost("Mass-schedule")]
        public async Task<IActionResult> MassCreateScheduleMeeting(ScheduleMeetingMassCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }


            Schedule schedule = await services.Meetings.MassCreateScheduleMeetingAsync(dto);
                            var mapped = mapper.Map<ScheduleGetDto>(schedule);
            //await services.Meetings.CreateScheduleMeetingAsync(dto);
            await groupHub.Clients.Group(dto.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg, $"{HttpContext.User.GetUsername()} tạo cuộc họp mới");
            return Ok(mapped);
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Create a new schedule meeting"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Schedule")]
        public async Task<IActionResult> CreateScheduleMeeting(ScheduleMeetingCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.Meetings.CreateScheduleMeetingAsync(dto);
            await groupHub.Clients.Group(dto.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg, $"{HttpContext.User.GetUsername()} tạo cuộc họp mới {dto.Name}");
            return Ok(dto);
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Start a schedule meeting"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Schedule/{id}/Start")]
        public async Task<IActionResult> StartSchdeuleMeeting(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            var meeting = await services.Meetings.GetByIdAsync(id);
            bool isJoining = await services.Groups.IsStudentJoiningGroupAsync(studentId, meeting.GroupId);
            if (!isJoining)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            if (meeting.End != null)
            {
                return BadRequest("Meeting đã kết thúc");
            }
            if (meeting.Start != null)
            {
                return BadRequest("Meeting đã bắt đầu");
            }
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId);
            //phải bắt đầu trong ngày schedule
            if (meeting.ScheduleStart.Value.Date > DateTime.Today)
            {
                return BadRequest($"Meeting được hẹn vào ngày {meeting.ScheduleStart.Value.Date.ToString("dd/MM")} Nếu muốn bắt đầu vào ngày hôm nay, hãy {(isLeader ? "cập nhật lại ngày hẹn" : "yêu  cầu nhóm trưởng cập nhật lại ngày hẹn")}");
            }
            //Member ko dc bắt sớm hơn
            if (meeting.ScheduleStart.Value > DateTime.Now && !isLeader)
            {
                return BadRequest($"Thành viên không thể bắt đầu meeting sớm hơn giờ hẹn. Nếu muốn bắt đầu ngay, hãy yêu  cầu nhóm trưởng bắt đầu cuộc họp");
            }
            meeting.Start = DateTime.Now;
            await services.Meetings.StartScheduleMeetingAsync(meeting);
            LiveMeetingGetDto dto = mapper.Map<LiveMeetingGetDto>(meeting);
            await groupHub.Clients.Group(dto.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg, $"{HttpContext.User.GetUsername()} bắt đầu cuộc họp {meeting.Name}");
            return Ok(dto);
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.No_Test}/{Auth.True}] Update a schedule meeting"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Schedule/{id}")]
        public async Task<IActionResult> UpdateScheduleMeeting(int id, ScheduleMeetingUpdateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            var meeting = await services.Meetings.GetByIdAsync(id);
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải thành viên của nhóm này");
            }
            dto.Date=dto.Date.AddDays(1);
            ValidatorResult valResult = await validators.Meetings.ValidateParams(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.Meetings.UpdateScheduleMeetingAsync(dto);
            var updatedDto = mapper.Map<ScheduleMeetingGetDto>(await services.Meetings.GetByIdAsync(id));
            await groupHub.Clients.Group(updatedDto.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg, $"{HttpContext.User.GetUsername()} cập nhật cuộc họp {updatedDto.Name}");
            return Ok(updatedDto);
        }


        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Remove a schedule meeting"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpDelete("Schedule/{id}")]
        public async Task<IActionResult> DeleteSchdeuleMeeting(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            var meeting = await services.Meetings.GetByIdAsync(id);
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, meeting.GroupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            if (meeting.End != null)
            {
                return BadRequest("Meeting đã kết thúc, không xóa được");
            }
            if (meeting.Start != null)
            {
                return BadRequest("Meeting đã bắt đầu, không xóa được");
            }
            //phải bắt đầu trong ngày schedule
            if (meeting.ScheduleStart.Value.Date < DateTime.Today)
            {
                return BadRequest("Meeting đã qua, không xóa được");
            }
            await services.Meetings.DeleteScheduleMeetingAsync(meeting);
            return Ok("Đã xóa meeting");
        }

        [SwaggerOperation(
           Summary = $"[{Actor.Parent}/{Finnished.True}/{Auth.True}] Remove a schedule meeting"
       )]
        [Authorize(Roles = Actor.Parent)]
        [HttpGet("Children")]
        public async Task<IActionResult> GetChildrenLiveMeeting()
        {
            int parentid = HttpContext.User.GetUserId();
            var list = services.Meetings.GetChildrenLiveMeetings(parentid);
            return Ok(list);
        }

       

        //// GET: api/Meetings/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Meeting>> GetMeeting(int id)
        //{
        //  if (services.Meetings == null)
        //  {
        //      return NotFound();
        //  }
        //    var meeting = await services.Meetings.FindAsync(id);

        //    if (meeting == null)
        //    {
        //        return NotFound();
        //    }

        //    return meeting;
        //}

        //// PUT: api/Meetings/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMeeting(int id, Meeting meeting)
        //{
        //    if (id != meeting.Id)
        //    {
        //        return BadRequest();
        //    }

        //    services.Entry(meeting).State = EntityState.Modified;

        //    try
        //    {
        //        await services.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MeetingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Meetings
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Meeting>> PostMeeting(Meeting meeting)
        //{
        //  if (services.Meetings == null)
        //  {
        //      return Problem("Entity set 'TempContext.Meetings'  is null.");
        //  }
        //    services.Meetings.Add(meeting);
        //    await services.SaveChangesAsync();

        //    return CreatedAtAction("GetMeeting", new { id = meeting.Id }, meeting);
        //}

        //// DELETE: api/Meetings/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteMeeting(int id)
        //{
        //    if (services.Meetings == null)
        //    {
        //        return NotFound();
        //    }
        //    var meeting = await services.Meetings.FindAsync(id);
        //    if (meeting == null)
        //    {
        //        return NotFound();
        //    }

        //    services.Meetings.Remove(meeting);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        private async Task<bool> MeetingExists(int id)
        {
            return (await services.Meetings.AnyAsync(id));
        }
    }
}
