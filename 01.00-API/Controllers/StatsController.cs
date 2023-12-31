using APIExtension.ClaimsPrinciple;
using APIExtension.Const;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using ShareResource.DTO;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly IRepoWrapper repos;
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;

        public StatsController(IRepoWrapper repos, IMapper mapper, IServiceWrapper services)
        {
            this.repos = repos;
            this.mapper = mapper;
            this.services = services;
        }

        #region old code
        //Get: api/Accounts/search
        //[SwaggerOperation(
        //    Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Student's stat by month"
        //    , Description = "lấy stat theo month" +
        //    "<br>month (yyyy-mm-dd): chỉ cần năm với tháng, day nhập đại hoặc không nhập"
        //)]
        //[HttpGet("{studentId}/{month}/old")]
        //[Authorize(Roles = Actor.Student_Parent)]
        //public IActionResult GetStatForStudentInMonthOld(int studentId, DateTime month)
        //{
        //    if (HttpContext.User.IsInRole(Actor.Student) && HttpContext.User.GetUserId() != studentId)
        //    {
        //        return Unauthorized("Bạn không thể xem dữ liệu của học sinh khác");
        //    }
        //    DateTime start = new DateTime(month.Year, month.Month, 1, 0, 0, 0).Date;
        //    DateTime end = start.AddMonths(1);
        //    //Nếu tháng này thì chỉ lấy past meeting
        //    IQueryable<Meeting> allMeetingsOfJoinedGroups = month.Month == DateTime.Now.Month
        //        ? repos.Meetings.GetList()
        //        .Include(c => c.Connections)
        //        .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
        //        .Where(e => ((e.ScheduleStart >= start && e.ScheduleStart.Value.Date < end) || (e.Start >= start && e.Start.Value.Date < end))
        //            && e.Group.GroupMembers.Any(gm => gm.AccountId == studentId)
        //            //lấy past meeting
        //            && (e.End != null || e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today))
        //        : repos.Meetings.GetList()
        //        .Include(c => c.Connections)
        //        .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
        //        .Where(c => c.ScheduleStart >= start && c.ScheduleStart.Value.Date < end
        //            && c.Group.GroupMembers.Any(gm => gm.AccountId == studentId));
        //    //int totalMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
        //    //    ? 0 : allMeetingsOfJoinedGroups.Count();
        //    int totalMeetingsCount = allMeetingsOfJoinedGroups.Count();
        //    var atendedMeetings = allMeetingsOfJoinedGroups
        //        .Where(e => e.Connections.Any(c => c.AccountId == studentId));
        //    int atendedMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
        //        ? 0 : allMeetingsOfJoinedGroups
        //        .Where(e => e.Connections.Any(c => c.AccountId == studentId)).Count();
        //    long totalMeetingTime = allMeetingsOfJoinedGroups.Count() == 0 ? 0
        //        : allMeetingsOfJoinedGroups.SelectMany(m => m.Connections)
        //            .Select(e => e.End.Value - e.Start).Select(ts => ts.Ticks).Sum();
        //    var timeSpan = new TimeSpan(totalMeetingTime);
        //    //var totalMeetingTime = allMeetingsOfJoinedGroups.SelectMany(m => m.Connections);//.Select(e=>e.End.Value-e.Start).Select(ts=>ts.Ticks).Sum(); 

        //    return Ok(new
        //    {
        //        TotalMeetings = allMeetingsOfJoinedGroups.ProjectTo<PastMeetingGetDto>(mapper.ConfigurationProvider),
        //        TotalMeetingsCount = totalMeetingsCount,
        //        AtendedMeetingsCount = atendedMeetingsCount,
        //        MissedMeetingsCount = totalMeetingsCount - atendedMeetingsCount,
        //        TotalMeetingTme = totalMeetingTime == 0 ? "Chưa tham gia buổi học nào"
        //            : $"{timeSpan.Hours} giờ {timeSpan.Minutes} phút {timeSpan.Seconds} giây"
        //    });
        //}
        #endregion

        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Student's stat by month"
            , Description = "lấy stat theo month" +
            "<br>month (yyyy-mm-dd): chỉ cần năm với tháng, day nhập đại"
        )]
        [HttpGet("{studentId}/{month}")]
        [Authorize(Roles = Actor.Student_Parent)]
        public async Task<IActionResult> GetStatForStudentInMonthNew(int studentId, DateTime month)
        {
            if (studentId < 0)
            {
                return NotFound();
            }
            if (HttpContext.User.IsInRole(Actor.Student) && HttpContext.User.GetUserId() != studentId)
            {
                return Unauthorized("Bạn không thể xem dữ liệu của học sinh khác");
            }
            StatGetDto mappedStat = await services.Stats.GetStatForStudentInMonth(studentId, month);
            return Ok(mappedStat);
        }

        [SwaggerOperation(
            Summary = $"[{Actor.Student_Parent}/{Finnished.False}/{Auth.True}] Student's stat by month"
            , Description = "lấy stat cho 5 tháng gần nhất"
        )]
        [HttpGet("{studentId}")]
        [Authorize(Roles = Actor.Student_Parent)]
        public async Task<IActionResult> GetStatForStudent(int studentId)
        {
            if (studentId < 0)
            {
                return NotFound();
            }
            if (HttpContext.User.IsInRole(Actor.Student) && HttpContext.User.GetUserId() != studentId)
            {
                return Unauthorized("Bạn không thể xem dữ liệu của học sinh khác");
            }
            IList<StatGetListDto> mappedStat = await services.Stats.GetStatsForStudent(studentId);
            return Ok(mappedStat);
        }
    }
}
