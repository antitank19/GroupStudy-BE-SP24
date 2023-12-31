using DataLayer.DBObject;
using ServiceLayer.Interface;
using ShareResource.DTO;
using ShareResource.Enums;

namespace APIExtension.Validator
{
    public interface IGroupMemberValidator
    {
        public Task<ValidatorResult> ValidateParamsAsync(GroupMemberInviteCreateDto? dto, int leaderId);
        public Task<ValidatorResult> ValidateParamsAsync(GroupMemberRequestCreateDto? dto);

    }

    public class GroupMemberValidator  : BaseValidator ,IGroupMemberValidator
    {
        private IServiceWrapper services;

        public GroupMemberValidator(IServiceWrapper services)
        {
            this.services = services;
        }

        public async Task<ValidatorResult> ValidateParamsAsync(GroupMemberInviteCreateDto? dto, int leaderId)
        {
            try
            {
                if (!await services.Groups.IsStudentLeadingGroupAsync(leaderId, dto.GroupId))
                {
                    validatorResult.Failures.Add("Bạn không phải nhóm trưởng nhóm này");
                }
                //if(await services.Groups.IsStudentJoiningGroupAsync(dto.AccountId, dto.GroupId))
                //{
                //    validatorResult.Failures.Add("Học sinh đã tham gia nhóm này");
                //}
                //if (await services.Groups.IsStudentInvitedToGroupAsync(dto.AccountId, dto.GroupId))
                //{
                //    validatorResult.Failures.Add("Học sinh đã được mời tham gia nhóm này từ trước");
                //}
                //if (await services.Groups.IsStudentRequestingToGroupAsync(dto.AccountId, dto.GroupId))
                //{
                //    validatorResult.Failures.Add("Học sinh đã yêu cầu tham gia nhóm này từ trước");
                //}
                //if (await services.Groups.IsStudentDeclinedToGroupAsync(dto.AccountId, dto.GroupId))
                //{
                //    validatorResult.Failures.Add("Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước");
                //}
                GroupMember exsited = await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
                if (exsited != null)
                {
                    switch (exsited.MemberRole)
                    {
                        case GroupMemberRole.Leader:
                            {
                                validatorResult.Failures.Add("Học sinh đã tham gia nhóm này");
                                break;
                            }
                        case GroupMemberRole.Member:
                            {
                                validatorResult.Failures.Add("Học sinh đã tham gia nhóm này");
                                break;
                            }
                            //Fix later
                        //case GroupMemberState.Inviting:
                        //    {
                        //        validatorResult.Failures.Add("Học sinh đã được mời tham gia nhóm này từ trước");
                        //        break;
                        //    }
                        //case GroupMemberState.Requesting:
                        //    {
                        //        validatorResult.Failures.Add("Học sinh đã yêu cầu tham gia nhóm này từ trước");
                        //        break;
                        //    }
                        //case GroupMemberRole.Banned:
                        //    {
                        //        validatorResult.Failures.Add("Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước");
                        //        break;
                        //    }
                        default:
                            {
                                validatorResult.Failures.Add("Học sinh đã có liên quan đến nhóm");
                                break;
                            }
                    }
                }
                //if (dto.InviteMessage == null || dto.InviteMessage.Trim().Length == 0)
                //{
                //    validatorResult.Failures.Add("Thiếu lời mời");
                //}
                //if (dto.InviteMessage == null || dto.InviteMessage.Trim().Length > 250)
                //{
                //    validatorResult.Failures.Add("Lời mời không thể dài hơn 250 kí tự");
                //}
            }
            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }

        public async Task<ValidatorResult> ValidateParamsAsync(GroupMemberRequestCreateDto? dto)
        {
            try
            {
                if (await services.Groups.IsStudentJoiningGroupAsync(dto.AccountId, dto.GroupId))
                {
                    validatorResult.Failures.Add("Bạn đã tham gia nhóm này");
                }
                //if (dto.RequestMessage == null || dto.RequestMessage.Trim().Length == 0)
                //{
                //    validatorResult.Failures.Add("Thiếu yêu cầu");
                //}
                //if (dto.RequestMessage == null || dto.RequestMessage.Trim().Length > 250)
                //{
                //    validatorResult.Failures.Add("Lời yêu cầu không thể dài hơn 250 kí tự");
                //}
            }
            catch (Exception ex)
            {
                validatorResult.Failures.Add(ex.Message);
            }
            return validatorResult;
        }
    }
}