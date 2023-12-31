using DataLayer.DBObject;
using ShareResource.DTO;

namespace ServiceLayer.Interface.Db
{
    public interface IMeetingService
    {
        public Task<bool> AnyAsync(int id);
        public IQueryable<PastMeetingGetDto> GetPastMeetingsForGroup(int groupId);
        public IQueryable<Meeting> GetScheduleMeetingsForGroup(int groupId);
        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForGroup(int groupId);
        public IQueryable<PastMeetingGetDto> GetPastMeetingsForStudent(int studentId);
        public IQueryable<PastMeetingGetDto> GetPastMeetingsForStudentByMonth(int studentId, DateTime  month);
        public IQueryable<ScheduleMeetingForMemberGetDto> GetScheduleMeetingsForStudent(int studentId);
        public IQueryable<ScheduleMeetingForMemberGetDto> GetScheduleMeetingsForStudentByDate(int studentId, DateTime date);
        public IQueryable<LiveMeetingGetDto> GetLiveMeetingsForStudent(int studentId);
        public Task<Meeting> GetByIdAsync(int id);
        public Task CreateScheduleMeetingAsync(ScheduleMeetingCreateDto dto);
        public Task<Schedule> MassCreateScheduleMeetingAsync(ScheduleMeetingMassCreateDto dto);
        public Task<Meeting> CreateInstantMeetingAsync(InstantMeetingCreateDto dto);
        public Task UpdateScheduleMeetingAsync(ScheduleMeetingUpdateDto dto);
        public Task StartScheduleMeetingAsync(Meeting meeting);
        public Task DeleteScheduleMeetingAsync(Meeting meeting);
        public IQueryable<ScheduleGetDto> GetSchedulesForGroup(int groupId);
        public IQueryable<ChildrenLiveMeetingGetDto> GetChildrenLiveMeetings(int parentId);
    }
}