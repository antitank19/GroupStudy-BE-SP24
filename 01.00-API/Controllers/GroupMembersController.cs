using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;
using ServiceLayer.Interface;
using ShareResource.DTO;
using APIExtension.Const;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using APIExtension.ClaimsPrinciple;
using APIExtension.Validator;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ShareResource.Enums;
using API.SignalRHub;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IValidatorWrapper validators;
        private readonly IMapper mapper;
        private readonly IHubContext<GroupHub> groupHub;

        public GroupMembersController(IServiceWrapper services, IValidatorWrapper validators, IMapper mapper, IHubContext<GroupHub> groupHub)
        {
            this.services = services;
            this.validators = validators;
            this.mapper = mapper;
            this.groupHub = groupHub;
        }

        //GET: api/GroupMember/Group/{groupId}
        [SwaggerOperation(
            Summary = $"[{Actor.Leader_Member}/{Finnished.True}/{Auth.True}] Get all members joining of group"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Group/{groupId}")]
        public async Task<IActionResult> GetJoinMembersForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isJoined = await services.Groups.IsStudentJoiningGroupAsync(studentId, groupId);
            if (!isJoined)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            IQueryable<AccountProfileDto> mapped = services.GroupMembers.GetMembersJoinForGroup(groupId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //GET: api/GroupMember/Invite/Group/groupId
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all invite of group for leader"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Invite/Group/{groupId}")]
        public async Task<IActionResult> GetInviteForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            IQueryable<JoinInviteForGroupGetDto> mapped = services.GroupMembers.GetJoinInviteForGroup(groupId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //GET: api/GroupMember/Request/Group/groupId
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get all join request of group for leader"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Request/Group/{groupId}")]
        public async Task<IActionResult> GetRequestForGroup(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            IQueryable<JoinRequestForGroupGetDto> mapped = services.GroupMembers.GetJoinRequestForGroup(groupId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //Post: api/GroupMember/Invite
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Create Invite to join group for leader"
            , Description ="Nhóm trưởng tạo thư mời vào nhóm<br>" +
                "groupId id của nhóm mời vào<br>" +
                "accountId id của học sinh được mời vào"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Invite")]
        public async Task<IActionResult> CreateInvite(GroupMemberInviteCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            #region unused code
            //if (await services.Groups.IsStudentJoiningGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã tham gia nhóm này");
            //    return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
            //}
            //if (await services.Groups.IsStudentInvitedToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã được mời tham gia nhóm này từ trước");
            //    GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>( 
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { Message = "Học sinh đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
            //}
            //if (await services.Groups.IsStudentRequestingToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã yêu cầu tham gia nhóm này từ trước");
            //    GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { Message = "Học sinh đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
            //}
            //if (await services.Groups.IsStudentDeclinedToGroupAsync(dto.AccountId, dto.GroupId))
            //{
            //    //validatorResult.Failures.Add("Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước");
            //    GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
            //        await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
            //    return BadRequest(new { 
            //        Message = "Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại", 
            //        Previous = getDto 
            //    });
            //}
            #endregion
            GroupMember exsited = await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
            if (exsited!=null) {
                if (!exsited.IsActive)
                {
                    GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                              await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    return BadRequest(new
                    {
                        Message = "Học sinh đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại",
                        Previous = getDto
                    });
                }
                switch (exsited.MemberRole)
                {
                    case GroupMemberRole.Leader:
                        {
                            return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
                        }
                    case GroupMemberRole.Member:
                        {
                            return BadRequest(new { Message = "Học sinh đã tham gia nhóm này" });
                        }
                        //Fix later
                    //case GroupMemberState.Inviting:
                    //    {
                    //        GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Học sinh đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
                    //    }
                    //case GroupMemberState.Requesting:
                    //    {
                    //        GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Học sinh đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
                    //    }
                    //case GroupMemberRole.Banned:
                    //    {
                            
                    //    }
                    default:
                        {
                            GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                                await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                            return BadRequest(new
                            {
                                Message = "Học sinh đã có liên quan đến nhóm",
                                Previous = getDto
                            });
                        }
                } 
            }
            ValidatorResult valResult = await validators.GroupMembers.ValidateParamsAsync(dto, studentId);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.GroupMembers.CreateJoinInvite(dto);

            return Ok();
        }

        //Put: api/GroupMember/Request/{requestId}/Accept"
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Accept join request for leader"
            , Description ="Nhóm trưởng chấp nhận request vào nhóm của học sinh"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Request/{requestId}/Accept")]
        public async Task<IActionResult> AcceptRequest(int requestId)
        {
            int studentId = HttpContext.User.GetUserId();
            Request existedRequest = await services.GroupMembers.GetRequestByIdAsync(requestId);
            if(existedRequest == null)
            {
                return BadRequest("Yêu cầu tham gia không tồn tại");
            }
            if (existedRequest.State == RequestStateEnum.Approved)
            {
                return BadRequest("Yêu cầu tham gia đã được chấp nhận");
            }
            if (existedRequest.State == RequestStateEnum.Decline)
                {
                return BadRequest("Yêu cầu tham gia đã bị từ chối");
            }
            GroupMember existedMember = await services.GroupMembers
                .GetGroupMemberOfStudentAndGroupAsync(existedRequest.AccountId ,existedRequest.GroupId);
            if (existedMember != null)
            {
                if (!existedMember.IsActive)
                {
                    return BadRequest("Học sinh đã bị đuổi khỏi nhóm");
                }
                return BadRequest("Học sinh đã tham gia nhóm nhóm");
            }
            if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, existedRequest.GroupId))
            {
                return BadRequest("Bạn không phải trưởng nhóm này");
            }
            //if (existed.State != GroupMemberState.Requesting)
            //{
            //    return BadRequest("Đây không phải yêu cầu");
            //}
            await services.GroupMembers.AcceptOrDeclineRequestAsync(existedRequest, true);
            return Ok();
        }


        //Put: api/GroupMember/Request/{requestId}/Decline"
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Decline join request for leader"
            , Description ="Nhóm trưởng từ chối request vào nhóm của học sinh"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Request/{requestId}/Decline")]
        public async Task<IActionResult> DeclineRequest(int requestId)
        {
            int studentId = HttpContext.User.GetUserId();
            Request existedRequest = await services.GroupMembers.GetRequestByIdAsync(requestId);
            if (existedRequest == null)
            {
                return BadRequest("Yêu cầu tham gia không tồn tại");
            }
            if (existedRequest.State == RequestStateEnum.Approved)
            {
                return BadRequest("Yêu cầu tham gia đã được chấp nhận");
            }
            if (existedRequest.State == RequestStateEnum.Decline)
            {
                return BadRequest("Yêu cầu tham gia đã bị từ chối");
            }
            GroupMember existedMember = await services.GroupMembers
                .GetGroupMemberOfStudentAndGroupAsync(existedRequest.AccountId, existedRequest.GroupId);
            if (existedMember != null)
            {
                if (!existedMember.IsActive)
                {
                    return BadRequest("Học sinh đã bị đuổi khỏi nhóm");
                }
                return BadRequest("Học sinh đã tham gia nhóm nhóm");
            }
            if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, existedRequest.GroupId))
            {
                return BadRequest("Bạn không phải trưởng nhóm này");
            }
            //if (existed.State != GroupMemberState.Requesting)
            //{
            //    return BadRequest("Đây không phải yêu cầu");
            //}
            await services.GroupMembers.AcceptOrDeclineRequestAsync(existedRequest, false);
            return Ok();
        }

        //GET: api/GroupMember/Invite/Student/{studentId}
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all join invite of student"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Invite/Student")]
        public async Task<IActionResult> GetInviteForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<JoinInviteForStudentGetDto> mapped = services.GroupMembers.GetJoinInviteForStudent(studentId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return base.Ok(mapped);
        }

        //GET: api/GroupMember/Request/Student/{studentId}
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Get all request of student"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Request/Student")]
        public async Task<IActionResult> GetRequestForStudent()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<JoinRequestForStudentGetDto> mapped = services.GroupMembers.GetJoinRequestForStudent(studentId);
            if (mapped == null || !mapped.Any())
            {
                return NotFound();
            }

            return Ok(mapped);
        }

        //POST: api/GroupMember/Request
        [SwaggerOperation(
            Summary = $"[{Actor.Student}/{Finnished.True}/{Auth.True}] Request to join new group for student"
            , Description ="Học sinh tạo request vào nhóm mới<br>" +
                "groupId: id của nhóm học sinh muốn vào<br>" +
                "accountId: id của học sinh đang muốn vào"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost("Request")]
        public async Task<IActionResult> CreateRequest(GroupMemberRequestCreateDto dto)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, dto.GroupId);
            if (studentId!=dto.AccountId)
            {
                return Unauthorized("Bạn không thể yêu cầu tham gia dùm người khác");
            }
            GroupMember exsitedGroupMember = await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
            if (exsitedGroupMember != null)
            {
                if (!exsitedGroupMember.IsActive)
                {
                    GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                             await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    return BadRequest(new
                    {
                        Message = "Bạn đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại",
                        Previous = getDto
                    });
                }
                switch (exsitedGroupMember.MemberRole)
                {
                    case GroupMemberRole.Leader:
                        {
                            return BadRequest(new { Message = "Bạn đã tham gia nhóm này" });
                        }
                    case GroupMemberRole.Member:
                        {
                            return BadRequest(new { Message = "Bạn đã tham gia nhóm này" });
                        }
                        //Fix later
                    //case GroupMemberState.Inviting:
                    //    {
                    //        GroupMemberInviteGetDto inviteGetDto = mapper.Map<GroupMemberInviteGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Bạn đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
                    //    }
                    //case GroupMemberState.Requesting:
                    //    {
                    //        GroupMemberRequestGetDto requestGetDto = mapper.Map<GroupMemberRequestGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new { Message = "Bạn đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
                    //    }
                    //case GroupMemberRole.Banned:
                    //    {
                    //        GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                    //            await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                    //        return BadRequest(new
                    //        {
                    //            Message = "Bạn đã từ chối/bị từ chối tham gia nhóm này từ trước. Hãy đợi tới tháng sau để thử lại",
                    //            Previous = getDto
                    //        });
                    //    }
                    default:
                        {
                            GroupMemberGetDto getDto = mapper.Map<GroupMemberGetDto>(
                                await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(dto.AccountId, dto.GroupId));
                            return BadRequest(new
                            {
                                Message = "Bạn đã có liên quan đến nhóm",
                                Previous = getDto
                            });
                        }
                }
            }
            Invite exsitedInvite = await services.GroupMembers.GetInviteOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
            if (exsitedInvite != null)
            {
                JoinInviteForGroupGetDto inviteGetDto = mapper.Map<JoinInviteForGroupGetDto>(exsitedInvite);
                return BadRequest(new { Message = "Bạn đã được mời tham gia nhóm này từ trước", Previous = inviteGetDto });
            }
            Request exsitedRequest = await services.GroupMembers.GetRequestOfStudentAndGroupAsync(dto.AccountId, dto.GroupId);
            if (exsitedRequest != null)
            {
                JoinRequestForGroupGetDto requestGetDto = mapper.Map<JoinRequestForGroupGetDto>(exsitedRequest);
                return BadRequest(new { Message = "Bạn đã yêu cầu tham gia nhóm này từ trước", Previous = requestGetDto });
            }

            ValidatorResult valResult = await validators.GroupMembers.ValidateParamsAsync(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            await services.GroupMembers.CreateJoinRequest(dto);
            await groupHub.Clients.Group(dto.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
            return Ok();
        }

        //Put: api/GroupMember/Invite/{inviteId}/Accept"
        [SwaggerOperation(
            Summary = $"[{Actor.Member}/{Finnished.True}/{Auth.True}] Accpet join invite for student"
            , Description ="Học sinh chấp nhận lời mời vào nhóm"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Invite/{inviteId}/Accept")]
        public async Task<IActionResult> AcceptInvite(int inviteId)
        {
            int studentId = HttpContext.User.GetUserId();
            Invite existedInvite = await services.GroupMembers.GetInviteByIdAsync(inviteId);
            if (existedInvite == null)
            {
                return BadRequest("Lời mời tham gia không tồn tại");
            }
            if (existedInvite.State == RequestStateEnum.Approved)
            {
                return BadRequest("Lời mời tham gia đã được chấp nhận");
            }
            if (existedInvite.State == RequestStateEnum.Decline)
            {
                return BadRequest("Lời mời tham gia đã bị từ chối");
            }
            GroupMember existedMember = await services.GroupMembers
                .GetGroupMemberOfStudentAndGroupAsync(existedInvite.AccountId, existedInvite.GroupId);
            if (existedMember != null)
            {
                if (!existedMember.IsActive)
                {
                    return BadRequest("Học sinh đã bị đuổi khỏi nhóm");
                }
                return BadRequest("Học sinh đã tham gia nhóm nhóm");
            }
            if (existedInvite.AccountId != studentId)
            {
                return BadRequest("Đây không phải lời mời cho bạn");
            }
            await services.GroupMembers.AcceptOrDeclineInviteAsync(existedInvite, true);
            await groupHub.Clients.Group(existedInvite.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
            return Ok();
        }


        //Put: api/GroupMember/Invite/{inviteId}/Decline"
        [SwaggerOperation(
           Summary = $"[{Actor.Member}/{Finnished.True}/{Auth.True}] Decline join invite for student"
           , Description = "Học sinh từ chối lời mời vào nhóm"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("Invite/{inviteId}/Decline")]
        public async Task<IActionResult> DeclineInvite(int inviteId)
        {
            int studentId = HttpContext.User.GetUserId();
            Invite existedInvite = await services.GroupMembers.GetInviteByIdAsync(inviteId);
            if (existedInvite == null)
            {
                return BadRequest("Lời mời tham gia không tồn tại");
            }
            if (existedInvite.State == RequestStateEnum.Approved)
            {
                return BadRequest("Lời mời tham gia đã được chấp nhận");
            }
            if (existedInvite.State == RequestStateEnum.Decline)
            {
                return BadRequest("Lời mời tham gia đã bị từ chối");
            }
            GroupMember existedMember = await services.GroupMembers
                .GetGroupMemberOfStudentAndGroupAsync(existedInvite.AccountId, existedInvite.GroupId);
            if (existedMember != null)
            {
                if (!existedMember.IsActive)
                {
                    return BadRequest("Học sinh đã bị đuổi khỏi nhóm");
                }
                return BadRequest("Học sinh đã tham gia nhóm nhóm");
            }
            if (existedInvite.AccountId != studentId)
            {
                return BadRequest("Đây không phải lời mời cho bạn");
            }
            await services.GroupMembers.AcceptOrDeclineInviteAsync(existedInvite, false);
            return Ok();
        }
        //Delete
        //: api/GroupMember/Invite/{inviteId}/Decline"
        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Banned user from group for leader"
           , Description = "Leader ban member khỏi nhóm<br>" +
                "groupId: id của nhóm<br>" +
                "banAccId: id của member bị ban"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpDelete("Group/{groupId}/Account/{banAccId}")]
        public async Task<IActionResult> BanMember(int groupId, int banAccId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLead = await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId);
            if (!isLead)
            {
                return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            }
            if(studentId == banAccId)
            {
                return Unauthorized("Bạn không thể đuổi chính mình");
            }
            GroupMember exited= await services.GroupMembers.GetGroupMemberOfStudentAndGroupAsync(banAccId, groupId);
            if(exited == null) 
            {
                return BadRequest("Học sinh không tham gia nhóm này");    
            }
            if(!exited.IsActive)
            {
                return BadRequest("Học sinh đã bị đuổi khỏi nhóm này");
            }
            await services.GroupMembers.BanUserFromGroupAsync(exited);
            await groupHub.Clients.Group(groupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
            return Ok();
        }
        private async Task<bool> GroupMemberExists(int id)
        {
            return (await services.GroupMembers.AnyAsync(id));
        }
    }
}
