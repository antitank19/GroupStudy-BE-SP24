using ShareResource.Utils;
using System.ComponentModel.DataAnnotations;

namespace ShareResource.DTO
{
    public class AccountChangePasswordDto : BaseUpdateDto
    {
        [Key]
        public int Id { get; set; }
        private string oldPassword;

        public string OldPassword
        {
            get { return oldPassword; }
            set { oldPassword = value.CustomHash(); }
        }

        //public string OldPassword { get; set }
        private string password;

        public string Password
        {
            get { return password; }
            set { password = value.CustomHash(); }
        }

        //public string Password { get; set; }
        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value.CustomHash(); }
        }

        //public string ConfirmPassword { get; set; }

    }
}
