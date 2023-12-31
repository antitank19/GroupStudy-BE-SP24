using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ShareResource.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.DBObject
{
    public class GroupMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public GroupMemberRole MemberRole { get; set; }
        //public string? InviteMessage { get; set; }
        //public string? RequestMessage { get; set; }
        public bool IsActive { get; set; } = true;
        //Group
        [ForeignKey("GroupId")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        //Student
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}