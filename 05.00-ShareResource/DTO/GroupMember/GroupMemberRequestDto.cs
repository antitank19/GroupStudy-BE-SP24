using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class JoinRequestForGroupGetDto
    {
        public int Id { get; set; }
        //public string RequestMessage { get; set; }   
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? Schhool { get; set; }
        public int Class { get; set; }
    }

    public class JoinRequestForStudentGetDto
    {
        public int Id { get; set; }
        //public string RequestMessage { get; set; }   
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public int MemberCount { get; set; }
        public int Class { get; set; }
        public ICollection<string> Subjects { get; set; } = new Collection<string>();
    }

    public class GroupMemberRequestCreateDto
    {
        //public string RequestMessage { get; set; }
        public int GroupId { get; set; }
        public int AccountId { get; set; }
    }
}
