using DataLayer.DBObject;
using ShareResource.DTO;
using ShareResource.FilterParams;
using ShareResource;

namespace RepositoryLayer.Interface
{
    public interface IMeetingRepository : IBaseRepo<Meeting, int>
    {
        //Task<Meeting> GetMeetingForConnection(string connectionId);
        //Task<Meeting> GetMeetingById(int roomId);
        //void RemoveConnection(Connection connection);
        //void AddMeeting(Meeting meeting);
        //Task<Meeting> DeleteMeeting(int id);
        //Task<Meeting> EditMeeting(int id, string newName);
        //Task DeleteAllRoom();
        //Task<PagedList<MeetingDto>> GetAllRoomAsync(RoomParams roomParams);
        //Task<MeetingDto> GetRoomDtoById(int roomId);
        //Task UpdateCountMember(int roomId, int count);
        //Task<Meeting> GetRoomForMeetingAsync(int meetingIdInt);
        Task<Meeting> GetMeetingByIdSignalr(int meetingId);
        Task<Meeting> GetMeetingForConnectionSignalr(string connectionId);
        Task EndConnectionSignalr(Connection connection);
        Task UpdateCountMemberSignalr(int meetingId, int count);
        IQueryable<Connection> GetActiveConnectionsForMeetingSignalr(int meetingId);
        Task EndMeetingSignalRAsync(Meeting meeting);
        Task<IEnumerable<Meeting>> MassCreateAsync(IEnumerable<Meeting> creatingMeetings);
    }
}