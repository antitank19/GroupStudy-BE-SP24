namespace ShareResource.DTO
{
    public class GroupDetailForLeaderGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
        public ICollection<StudentGetDto> Members { get; set; }
        public ICollection<JoinRequestForGroupGetDto> JoinRequest { get; set; }
        public ICollection<JoinInviteForGroupGetDto> JoinInvite { get; set; }
        public ICollection<GroupMemberGetDto> DeclineRequest { get; set; }
        public virtual ICollection<PastMeetingGetDto> PastMeetings { get; set; }
        public virtual ICollection<LiveMeetingGetDto> LiveMeetings { get; set; }
        public virtual ICollection<ScheduleMeetingForLeaderGetDto> ScheduleMeetings { get; set; }
        public ICollection<SubjectGetDto> Subjects { get; set; }

    }
}
