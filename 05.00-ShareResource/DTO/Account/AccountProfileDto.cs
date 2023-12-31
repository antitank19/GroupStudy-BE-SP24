using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ShareResource.DTO
{
    public class AccountProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }

        public string RoleName { get; set; }
        public string? Schhool { get; set; }
        public DateTime? DateOfBirth { get; set; }

        //Parent
        public virtual ICollection<string> Parents { get; set; }
        public virtual ICollection<string> Students { get; set; }

        // Group Member
        public virtual ICollection<GroupGetListDto> LeadGroups { get; set; }// = new Collection<GroupGetListDto>();
        public virtual ICollection<GroupGetListDto> JoinGroups { get; set; }// = new Collection<GroupGetListDto>();
    }
}
