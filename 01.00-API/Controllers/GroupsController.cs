using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using DataLayer.DBObject;
using DataLayer.DBContext;
using Swashbuckle.AspNetCore.Annotations;
using APIExtension.Const;
using ServiceLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using APIExtension.ClaimsPrinciple;
using ShareResource.UpdateApiExtension;
using ShareResource.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using APIExtension.Validator;
using API.SignalRHub;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IServiceWrapper services;
        private readonly IMapper mapper;
        private readonly IValidatorWrapper validators;

        public GroupsController(IServiceWrapper services, IMapper mapper, IValidatorWrapper validators)
        {
            this.services = services;
            this.mapper = mapper;
            this.validators = validators;
        }

        // GET: api/Groups/Join
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Search list of groups for student",
           Description = "Search group theo name, class, môn, id<br>" +
                "newGroup thành true nếu không muốn search những nhóm cũ (nên để)"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Search")]
        public async Task<IActionResult> SearchGroup(string search, bool newGroup=true)
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.SearchGroups(search, studentId, newGroup);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        [SwaggerOperation(
          Summary = $"[{Actor.Student}/{Finnished.True}]Search list of groups by classs for student",
          Description = "Search group theo class<br>" +
               "newGroup thành true nếu không muốn search những nhóm cũ (nên để)"
      )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Search/Class")]
        public async Task<IActionResult> SearchGroupByClass(string Class, bool newGroup = true)
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.SearchGroupsByClass(Class, studentId, newGroup);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        [SwaggerOperation(
          Summary = $"[{Actor.Student}/{Finnished.True}]Search list of groups by subject for student",
          Description = "Search group theo class<br>" +
               "newGroup thành true nếu không muốn search những nhóm cũ (nên để)"
      )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Search/Subject")]
        public async Task<IActionResult> SearchGroupBySubject(string subject, bool newGroup = true)
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.SearchGroupsBySubject(subject, studentId, newGroup);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Join
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get list of groups student joined",
           Description = "Lấy list nhóm mà học sinh đã tham gia (role là leader hoặc member)"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Join")]
        public async Task<IActionResult> GetJoinedGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetJoinGroupsOfStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Member
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get list of groups student joined as a member",
           Description = "Lấy list nhóm mà học sinh đã tham gia (role member)"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Member")]
        public async Task<IActionResult> GetMemberGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetMemberGroupsOfStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Member
        [SwaggerOperation(
           Summary = $"[{Actor.Student}/{Finnished.True}]Get group detail for a member",
           Description = "Lấy chi tiết group cho member (ít thông tin hơn)"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpGet("Member/{groupId}")]
        public async Task<IActionResult> GetGroupDetailForMember(int groupId)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentMemberGroupAsync(studentId, groupId);
            if (!isLeader)
            {
                return Unauthorized("Bạn không phải là thành viên nhóm này");
            }
            Group group = await services.Groups.GetFullByIdAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }
            GroupGetDetailForMemberDto dto = mapper.Map<GroupGetDetailForMemberDto>(group);
            return Ok(dto);
        }

        // GET: api/Groups/Lead
        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}] Get list of groups where student is leader",
           Description = "Lấy list nhóm mà học sinh đã tạo(role leader)"
       )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Lead")]
        public async Task<IActionResult> GetLeadGroups()
        {
            int studentId = HttpContext.User.GetUserId();
            IQueryable<Group> list = await services.Groups.GetLeaderGroupsOfStudentAsync(studentId);
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        // GET: api/Groups/Lead/5
        [SwaggerOperation(
            Summary = $"[{Actor.Leader}/{Finnished.True}/{Auth.True}] Get group detail for leader by Id",
           Description = "Lấy chi tiết group cho leader (nhiều thông tin hơn)"
        )]
        [Authorize(Roles =Actor.Student)]
        [HttpGet("Lead/{id}")]
        public async Task<IActionResult> GetGroupDetailForLeader(int id)
        {
            int studentId = HttpContext.User.GetUserId();
            bool isLeader = await services.Groups.IsStudentLeadingGroupAsync(studentId, id);
            //if (!isLeader)
            //{
            //     return Unauthorized("Bạn không phải nhóm trưởng của nhóm này");
            //}
            bool isJoining = await services.Groups.IsStudentJoiningGroupAsync(studentId, id);
            if (!isJoining)
            {
                return Unauthorized("Bạn không phải thành viên của nhóm này");
            }
            Group group = await services.Groups.GetFullByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }
            if (isLeader)
            {
                GroupDetailForLeaderGetDto dto = mapper.Map<GroupDetailForLeaderGetDto>(group);
                return Ok(dto);
            }
            else
            {
                GroupGetDetailForMemberDto dto = mapper.Map<GroupGetDetailForMemberDto>(group);
                return Ok(dto); 
            }
            
        }

        // POST: api/Groups
        [SwaggerOperation(
           Summary = $"[{Actor.Leader}/{Finnished.True}] create new group for leader",
           Description = "create new group for leader<br>" +
                "Ai tạo nhóm sẽ trở thành trưởng nhóm"
       )]
        [Authorize(Roles = Actor.Student)]
        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(GroupCreateDto dto)
        {
            int creatorId = HttpContext.User.GetUserId();
            ValidatorResult valResult = await validators.Groups.ValidateParams(dto);
            if(!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }
            Group group = mapper.Map<Group>(dto);
            await services.Groups.CreateAsync(group, creatorId);

            return Ok();
        }

        // PUT: api/Groups/5
        [SwaggerOperation(
         Summary = $"[{Actor.Leader}/{Finnished.True}] Update group for leader",
         Description = "groupId Id nhóm dc update"
        )]
        [Authorize(Roles = Actor.Student)]
        [HttpPut("{groupId}")]
        public async Task<IActionResult> UpdateGroup(int groupId, GroupUpdateDto dto)
        {
            if (groupId != dto.Id)
            {
                return BadRequest();
            }
            int studentId = HttpContext.User.GetUserId();
            //List<int> leadGroupIds = (await services.Groups.GetLeaderGroupsIdAsync(studentId));

            if (!await services.Groups.IsStudentLeadingGroupAsync(studentId, groupId))
            {
                return Unauthorized("You can't update other's group");
            }
            ValidatorResult valResult = await validators.Groups.ValidateParams(dto);
            if (!valResult.IsValid)
            {
                return BadRequest(valResult.Failures);
            }

            var group = await services.Groups.GetFullByIdAsync(groupId);
            if (group == null)
            {
                return NotFound();
            }
            try
            {
                
                await services.Groups.UpdateAsync(dto);
                var mapped = mapper.Map<GroupDetailForLeaderGetDto>(group);
                return Ok(mapped);
            }
            catch (Exception ex)
            {
                if (!await GroupExists(groupId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: api/Groups
        [Tags(Actor.Test)]
        [SwaggerOperation(
           Summary = $"[{Actor.Test}/{Finnished.True}]Get list of groups",
           Description = "Get leadGroupIds of group"
       )]
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            IQueryable<Group> list = services.Groups.GetList();
            if (list == null || !list.Any())
            {
                return NotFound();
            }
            var mapped = list.ProjectTo<GroupGetListDto>(mapper.ConfigurationProvider);
            return Ok(mapped);
        }

        [Tags(Actor.Test)]
        [SwaggerOperation(
        Summary = $"[{Actor.Test}/{Finnished.True}] Get group by Id",
        Description = "Get group by Id"
    )]
        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            Group group = await services.Groups.GetFullByIdAsync(id);

            if (group == null)
            {
                return NotFound();
            }
            GroupDetailForLeaderGetDto dto = mapper.Map<GroupDetailForLeaderGetDto>(group);
            return Ok(dto);
        }

        //// DELETE: api/Groups/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGroup(int id)
        //{
        //    if (services.Groups == null)
        //    {
        //        return NotFound();
        //    }
        //    var @dto = await services.Groups.FindAsync(id);
        //    if (@dto == null)
        //    {
        //        return NotFound();
        //    }

        //    services.Groups.Remove(@dto);
        //    await services.SaveChangesAsync();

        //    return NoContent();
        //}

        private async Task<bool> GroupExists(int id)
        {
            return (await services.Groups.GetFullByIdAsync(id)) is not null;
        }
    }
}
