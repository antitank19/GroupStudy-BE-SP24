using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class JoinInviteForGroupGetDto
    {
        public int Id { get; set; }
        //public string InviteMessage { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? Schhool { get; set; }
        public int Class { get; set; }
    }

    public class JoinInviteForStudentGetDto
    {
        public int Id { get; set; }
        //public string InviteMessage { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public int MemberCount { get; set; }
        public int Class { get; set; }
        public ICollection<string> Subjects { get; set; } = new Collection<string>();
    }

    public class GroupMemberInviteCreateDto
    {
        //public string InviteMessage { get; set; }
        [Required]
        public int GroupId { get; set; }
        public int AccountId { get; set; }
    }
}
