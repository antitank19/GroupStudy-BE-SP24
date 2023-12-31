using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class AccountUpdateDto  : BaseUpdateDto
    {
        [Key]
        public int Id { get; set; }

        private string? fullName;
        public string? FullName
        {
            get { return fullName?.Trim(); }
            set { fullName = value?.Trim(); }
        }

        private string? phone;
        public string? Phone
        {
            get { return phone?.Trim(); }
            set { phone = value?.Trim(); }
        }

        private string? schhool { get; set; }
        public string? Schhool
        {
            get { return schhool; }
            set { schhool = value.Trim(); }
        }


        private DateTime? dateOfBirth;

        public DateTime? DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value.Value.Date; }
        }

    }
}
