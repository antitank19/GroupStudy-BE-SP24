using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class MemberSignalrDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName => UserName;
        public string FullName { get; set; }
        //public DateTime LastActive { get; set; }
        //public string PhotoUrl { get; set; }
        //public bool Locked { get; set; }
    }
}
