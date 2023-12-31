using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class PastMeetingGetDto : BaseGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime? ScheduleStart { get; set; }
        public DateTime? ScheduleEnd { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CountMember { get; set; }
        public ICollection<ChatGetDto> Chats { get; set; }
        public ICollection<ReviewSignalrDTO> Reviews { get; set; }
    }
}
