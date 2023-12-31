using AutoMapper.QueryableExtensions;
using AutoMapper;
using DataLayer.DBObject;
using ShareResource.DTO;
using RepositoryLayer.Interface;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using Microsoft.Extensions.Primitives;

namespace ServiceLayer.ClassImplement.Db
{
    public class StatService : IStatService
    {
        private IRepoWrapper repos;
        private readonly IMapper mapper;

        public StatService(IRepoWrapper repos, IMapper mapper)
        {
            this.repos = repos;
            this.mapper = mapper;
        }

        public async Task<StatGetDto> GetStatForStudentInMonth(int studentId, DateTime month)
        {
            DateTime start = new DateTime(month.Year, month.Month, 1, 0, 0, 0).Date;
            DateTime end = start.AddMonths(1);

            Account student=await repos.Accounts.GetByIdAsync(studentId);

            //Nếu tháng này thì chỉ lấy past meeting
            IQueryable<Meeting> allMeetingsOfJoinedGroups = month.Month == DateTime.Now.Month
                ? repos.Meetings.GetList()
                .Include(m=>m.Chats).ThenInclude(c=>c.Account)
                .Include(c => c.Connections)
                .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
                .Include(m=>m.Reviews).ThenInclude(r=>r.Details)
                .Where(e => ((e.ScheduleStart >= start && e.ScheduleStart.Value.Date < end) || (e.Start >= start && e.Start.Value.Date < end))
                    && e.Group.GroupMembers.Any(gm => gm.AccountId == studentId)
                    //lấy past meeting
                    && (e.End != null || e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today))
                : repos.Meetings.GetList()
                .Include(m=>m.Chats).ThenInclude(c=>c.Account)
                .Include(c => c.Connections)
                .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
                .Include(m=>m.Reviews).ThenInclude(r=>r.Details)
                .Where(c => c.ScheduleStart >= start && c.ScheduleStart.Value.Date < end
                    && c.Group.GroupMembers.Any(gm => gm.AccountId == studentId));
            //int totalMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
            //    ? 0 : allMeetingsOfJoinedGroups.Count();
            int totalMeetingsCount = allMeetingsOfJoinedGroups.Count();
            IQueryable<Meeting> atendedMeetings = allMeetingsOfJoinedGroups
                .Where(e => e.Connections.Any(c => c.AccountId == studentId));
            //int atendedMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
            //    ? 0 : allMeetingsOfJoinedGroups
            //    .Where(e => e.Connections.Any(c => c.AccountId == studentId)).Count();
            int atendedMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
                ? 0 : atendedMeetings.Count();
            long totalMeetingTime = atendedMeetings.Count() == 0 ? 0
                : atendedMeetings.SelectMany(m => m.Connections)
                    .Select(e => e.End.Value - e.Start).Select(ts => ts.Ticks).Sum();
            TimeSpan timeSpan = new TimeSpan(totalMeetingTime);
            IQueryable<ReviewDetail> reviewDetails = atendedMeetings
                .SelectMany(m => m.Reviews)
                .SelectMany(r => r.Details);
            var averageVoteResult = !reviewDetails.Any()? 0
                : await reviewDetails.Select(e => (int)e.Result).AverageAsync();
            //var totalMeetingTime = allMeetingsOfJoinedGroups.SelectMany(m => m.Connections);//.Select(e=>e.End.Value-e.Start).Select(ts=>ts.Ticks).Sum(); 

            return new StatGetDto
            {
                StudentFullname=student.FullName,
                StudentUsername=student.Username,
                Month = start,
                //TotalMeetings = allMeetingsOfJoinedGroups.ProjectTo<PastMeetingGetDto>(mapper.ConfigurationProvider),
                TotalMeetingsCount = totalMeetingsCount,
                AtendedMeetings = atendedMeetings.ProjectTo<PastMeetingGetDto>(mapper.ConfigurationProvider),
                AtendedMeetingsCount = atendedMeetingsCount,
                MissedMeetingsCount = totalMeetingsCount - atendedMeetingsCount,
                TotalMeetingTme = totalMeetingTime == 0 ? "Chưa tham gia buổi học nào"
                    : $"{timeSpan.Hours} giờ {timeSpan.Minutes} phút {timeSpan.Seconds} giây",
                AverageVoteResult = averageVoteResult
            };
        }
        public async Task<IList<StatGetListDto>> GetStatsForStudent(int studentId)
        {
            DateTime month = DateTime.Now;
            List<StatGetListDto> stats = new List<StatGetListDto>();
            List<DateTime> startDates = new List<DateTime>(); 
            DateTime start1 = new DateTime(month.Year, month.Month, 1, 0, 0, 0).Date;
            startDates.Add(start1);
            Account student = await repos.Accounts.GetByIdAsync(studentId);
            for(int i=1; i<=4; i++)
            {
                startDates.Add(start1.AddMonths(-i));
            }

            foreach(DateTime start in startDates)
            {
                DateTime end = start.AddMonths(1);
                //Nếu tháng này thì chỉ lấy past meeting
                IQueryable<Meeting> allMeetingsOfJoinedGroups = start.Month == DateTime.Now.Month
                    ? repos.Meetings.GetList()
                    .Include(m => m.Chats).ThenInclude(c => c.Account)
                    .Include(c => c.Connections)
                    .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
                    .Include(m => m.Reviews).ThenInclude(r => r.Details)
                    .Where(e => ((e.ScheduleStart >= start && e.ScheduleStart.Value.Date < end) || (e.Start >= start && e.Start.Value.Date < end))
                        && e.Group.GroupMembers.Any(gm => gm.AccountId == studentId)
                        //lấy past meeting
                        && (e.End != null || e.ScheduleStart != null && e.ScheduleStart.Value.Date < DateTime.Today))
                    : repos.Meetings.GetList()
                    .Include(m => m.Chats).ThenInclude(c => c.Account)
                    .Include(c => c.Connections)
                    .Include(m => m.Group).ThenInclude(g => g.GroupMembers)
                    .Include(m => m.Reviews).ThenInclude(r => r.Details)
                    .Where(c => c.ScheduleStart >= start && c.ScheduleStart.Value.Date < end
                        && c.Group.GroupMembers.Any(gm => gm.AccountId == studentId));
                int totalMeetingsCount = allMeetingsOfJoinedGroups.Count();
                IQueryable<Meeting> atendedMeetings = allMeetingsOfJoinedGroups
                    .Where(e => e.Connections.Any(c => c.AccountId == studentId));
                int atendedMeetingsCount = allMeetingsOfJoinedGroups.Count() == 0
                    ? 0 : atendedMeetings.Count();
                long totalMeetingTime = atendedMeetings.Count() == 0 ? 0
                    : atendedMeetings.SelectMany(m => m.Connections)
                        .Select(e => e.End.Value - e.Start).Select(ts => ts.Ticks).Sum();
                TimeSpan timeSpan = new TimeSpan(totalMeetingTime);
                IQueryable<ReviewDetail> reviewDetails = atendedMeetings
                    .SelectMany(m => m.Reviews)
                    .SelectMany(r => r.Details);
                var averageVoteResult = !reviewDetails.Any() ? 0
                    : await reviewDetails.Select(e => (int)e.Result).AverageAsync();

                StatGetListDto newStat = new StatGetListDto
                {
                    StudentFullname = student.FullName,
                    StudentUsername = student.Username,
                    Month = start,
                    TotalMeetingsCount = totalMeetingsCount,
                    AtendedMeetingsCount = atendedMeetingsCount,
                    MissedMeetingsCount = totalMeetingsCount - atendedMeetingsCount,
                    //TotalMeetingTme = totalMeetingTime == 0 ? "Chưa tham gia buổi học nào"
                    //    : $"{timeSpan.Hours} giờ {timeSpan.Minutes} phút {timeSpan.Seconds} giây",
                    TotalMeetingTme = timeSpan,
                    AverageVoteResult = averageVoteResult
                };
                stats.Add(newStat);
            }



           
            return stats;
        }
    }
}
