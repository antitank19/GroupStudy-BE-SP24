using ServiceLayer.Interface;
using ShareResource.DTO;

namespace APIExtension.Validator
{
    public interface IGroupValidator
    {
        public Task<ValidatorResult> ValidateParams(GroupCreateDto dto);
        public Task<ValidatorResult> ValidateParams(GroupUpdateDto dto);
    }
    public class GroupValidator : BaseValidator, IGroupValidator
    {
        private IServiceWrapper services;

        public GroupValidator(IServiceWrapper services)
        {
            this.services = services;
        }

        public async Task<ValidatorResult> ValidateParams(GroupCreateDto dto)
        {
            try
            {
                //Name
                if (dto.Name.Trim().Length == 0)
                {
                    validatorResult.Failures.Add("Thiếu tên nhóm");
                }
                if (dto.Name.Trim().Length > 50)
                {
                    validatorResult.Failures.Add("Tên nhóm quá dài");
                }
                //Class
                if (dto.ClassId < 6 || dto.ClassId > 12)
                {
                    validatorResult.Failures.Add($"Lớp {dto.ClassId} không tồn tại");
                }
                //Subject
                if (!dto.SubjectIds.Any())
                {
                    validatorResult.Failures.Add("Thiếu môn học");
                }
                if (dto.SubjectIds.Any(id => (int)id < 1 || (int)id > 13))
                {
                    validatorResult.Failures.Add($"Môn học không tồn tại");
                }

            }
            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }

        public async Task<ValidatorResult> ValidateParams(GroupUpdateDto dto)
        {
            try
            {
                //Name
                //Nếu null thì ko update
                if (dto.Name != null)
                {
                    if (dto.Name.Trim().Length == 0)
                    {
                        validatorResult.Failures.Add("Thiếu tên nhóm");
                    }
                    if (dto.Name.Trim().Length > 50)
                    {
                        validatorResult.Failures.Add("Tên nhóm quá dài");
                    }
                }
                //Class
                if (dto != null)
                {
                    if (dto.ClassId < 6 || dto.ClassId > 12)
                    {
                        validatorResult.Failures.Add($"Lớp {dto.ClassId} không tồn tại");
                    }
                }
                //Subject
                if (dto.SubjectIds != null)
                {
                    if (!dto.SubjectIds.Any())
                    {
                        validatorResult.Failures.Add("Thiếu môn học");
                    }
                    if (dto.SubjectIds.Any(id => (int)id < 1 || (int)id > 13))
                    {
                        validatorResult.Failures.Add($"Môn học không tồn tại");
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