using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareResource.Utils;

namespace ShareResource.DTO
{
    public class StudentRegisterDto : BaseCreateDto
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
        }

        [EmailAddress]
        public string Email { get; set; }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value.Trim(); }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value.Trim(); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value.Trim(); }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value.Trim(); }
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

        public int ClassId { get; set; } = 6;
        public bool IsStudent { get; set; } = true;

        //Role
        //[ForeignKey("RoleId")]
        //public int RoleId { get; set; }
    }
    public class ParentRegisterDto : BaseCreateDto
    {
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value.Trim(); }
        }

        [EmailAddress]
        public string Email { get; set; }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value.Trim(); }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value.Trim(); }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set { fullName = value.Trim(); }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set { phone = value.Trim(); }
        }

        private DateTime? dateOfBirth;

        public DateTime? DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value.Value.Date; }
        }


        //Role
        //[ForeignKey("RoleId")]
        //public int RoleId { get; set; }
    }
}
