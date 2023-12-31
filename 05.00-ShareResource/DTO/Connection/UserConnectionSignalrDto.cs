using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO.Connection
{
    public class UserConnectionSignalrDto
    {
        public UserConnectionSignalrDto() { }
        public UserConnectionSignalrDto(string userName, int roomId)
        {
            Username = userName;
            MeetingId = roomId;
        }
        public string Username { get; set; }
        public int MeetingId { get; set; }
    }
}
