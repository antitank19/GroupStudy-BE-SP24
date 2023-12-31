using ShareResource.DTO.Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class ChildrenLiveMeetingGetDto
    {
        public int ChildId { get; set; }
        public string ChildUsername { get; set; }
        public string ChildFullName { get; set; }
        public ICollection<LiveMeetingGetDto> LiveMeetings { get; set; }
        //public ICollection<TempConnectionDto> Connections { get; set; }
        //public IQueryable<UserConnectionSignalrDto> Connections { get; set; }
        //= new IQueryable<LiveMeetingGetDto>();
    }
    public class TempConnectionDto
    {
        public LiveMeetingGetDto Meeting { get; set; }
    }

}
