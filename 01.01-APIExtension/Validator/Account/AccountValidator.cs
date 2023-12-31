using ServiceLayer.Interface;
using ShareResource.DTO;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace APIExtension.Validator
{
    public interface IAccountValidator
    {
        Task<ValidatorResult> ValidateParams(AccountUpdateDto dto);
        Task<ValidatorResult> ValidateParams(StudentRegisterDto dto);
        Task<ValidatorResult> ValidateParams(ParentRegisterDto dto);
    }
    public class AccountValidator : BaseValidator, IAccountValidator
    {
        private IServiceWrapper services;

        public AccountValidator(IServiceWrapper services)
        {
            this.services = services;
        }
        //^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$
        //^[0-9]{8,20}$
        Regex phoneRegex = new Regex(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$");

        public async Task<ValidatorResult> ValidateParams(StudentRegisterDto dto)
        {
            try
            {
                //username
                if(await services.Accounts.ExistUsernameAsync(dto.Username))
                {
                    validatorResult.Failures.Add("Tên tài khoản đã tồn tại");
                }
                if (dto.Username.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu tên tài khoản");
                }
                if (dto.Username.Trim().Length > 32)
                {
                    validatorResult.Failures.Add("Tên tài khoản quá dài");
                }
                //email                                                   
                if (await services.Accounts.ExistEmailAsync(dto.Email))
                {
                    validatorResult.Failures.Add("Email đã tồn tại");
                }
                if (dto.Email.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu email");
                }
                //password   
                if (dto.Password.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu mật khẩu");
                }
                if (dto.Password.Length > 32)
                {
                    validatorResult.Failures.Add("Mật khẩu quá dài");
                }
                if (dto.Password != dto.ConfirmPassword)
                {
                    validatorResult.Failures.Add("Xác nhận mật khẩu không thành công");
                }
                //name
                if (dto.FullName.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu họ tên");
                }
                if (dto.FullName.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Họ tên quá dài");
                }
                //sđt
                if (dto.Phone.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu số điện thoại");
                }
                if (dto.Phone.Trim().Length > 20)
                {
                    validatorResult.Failures.Add("Số điện thoại quá dài");
                }
                if (!phoneRegex.IsMatch(dto.Phone))
                {
                    validatorResult.Failures.Add("Số điện thoại không đúng định dạng");
                }
                //Dob
                if (dto.DateOfBirth != null)
                {
                    if(dto.DateOfBirth.Value > DateTime.Today)
                    {
                        validatorResult.Failures.Add("Ngày sinh không hợp lệ");
                    }
                }
                //School
                //if (dto.Schhool != null)
                //{
                //    if (dto.Schhool.Trim().Length == 0)
                //    {
                //        validatorResult.Failures.Add("Thiếu tên trường");
                //    }
                //}
            }

            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }

        public async Task<ValidatorResult> ValidateParams(ParentRegisterDto dto)
        {
            try
            {
                //username
                if (await services.Accounts.ExistUsernameAsync(dto.Username))
                {
                    validatorResult.Failures.Add("Tên tài khoản đã tồn tại");
                }
                if (dto.Username.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu tên tài khoản");
                }
                if (dto.Username.Trim().Length > 32)
                {
                    validatorResult.Failures.Add("Tên tài khoản quá dài");
                }
                //email                                                   
                if (await services.Accounts.ExistEmailAsync(dto.Email))
                {
                    validatorResult.Failures.Add("Email đã tồn tại");
                }
                if (dto.Email.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu email");
                }
                //password   
                if (dto.Password.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu mật khẩu");
                }
                if (dto.Password.Length > 32)
                {
                    validatorResult.Failures.Add("Mật khẩu quá dài");
                }
                if (dto.Password != dto.ConfirmPassword)
                {
                    validatorResult.Failures.Add("Xác nhận mật khẩu không thành công");
                }
                //name
                if (dto.FullName.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu họ tên");
                }
                if (dto.FullName.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Họ tên quá dài");
                }
                //sđt
                if (dto.Phone.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu số điện thoại");
                }
                if (dto.Phone.Trim().Length > 20)
                {
                    validatorResult.Failures.Add("Số điện thoại quá dài");
                }
                if (!phoneRegex.IsMatch(dto.Phone))
                {
                    validatorResult.Failures.Add("Số điện thoại không đúng định dạng");
                }
                //Dob
                if (dto.DateOfBirth != null)
                {
                    if (dto.DateOfBirth.Value > DateTime.Today)
                    {
                        validatorResult.Failures.Add("Ngày sinh không hợp lệ");
                    }
                }
            }

            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }

        public async Task<ValidatorResult> ValidateParams(AccountUpdateDto dto)
        {
            try
            {
                if (!await services.Accounts.ExistAsync(dto.Id))
                {
                    validatorResult.Failures.Add("Tài khoản không tồn tại");
                }
                //Nếu null thì ko update
                if (dto.FullName != null && dto.FullName.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu họ tên");
                }
                if (dto.FullName != null && dto.FullName.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Họ tên quá dài");
                }
                if (dto.Phone != null && dto.Phone.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu số điện thoại");
                }
                if (dto.Phone != null && !phoneRegex.IsMatch(dto.Phone))
                {
                    validatorResult.Failures.Add("Số điện thoại không đúng định dạng");
                }
                //Dob
                if (dto.DateOfBirth != null)
                {
                    if (dto.DateOfBirth.Value > DateTime.Today)
                    {
                        validatorResult.Failures.Add("Ngày sinh không hợp lệ");
                    }
                }
            }

            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }
    }
}