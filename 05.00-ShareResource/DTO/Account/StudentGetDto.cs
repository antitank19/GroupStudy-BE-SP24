using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class StudentGetDto  :BaseGetDto
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string? Schhool { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //Role
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public virtual RoleGetDto Role { get; set; }

        //// Group Member
        //public virtual ICollection<GroupMemberGetDto> GroupMember { get; set; }
    }
}
