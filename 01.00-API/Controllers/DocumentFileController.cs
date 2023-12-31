using API.SignalRHub;
using APIExtension.ClaimsPrinciple;
using APIExtension.Const;
using AutoMapper;
using DataLayer.DBObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ServiceLayer.Interface;
using ShareResource.DTO.File;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentFileController : ControllerBase
{
    private readonly IServiceWrapper _service;
    private readonly IMapper _mapper;
    private readonly IHubContext<GroupHub> groupHub;

    //Path của thư mục chứa file
    private const string path = "UploadFile\\";
    //private const string path = "https://ample-definitely-reptile.ngrok-free.app/";

    //private const string path = "s3://arn:aws:s3:ap-southeast-1:952968050037/";

    // dòng này đổi thành host của máy
    // bỏ comment dòng này nếu chạy trên local
    private const string HostUploadFile = "https://shin198-001-site1.ctempurl.com/uploadfile/";
    //private const string HostUploadFile = "arn:aws:s3:ap-southeast-1:952968050037:accesspoint/group-study-storage/";

    // dòng này dùng với ngrok, copy link của ngrok thay biến bên dưới
    // host do ngrok thay đổi mỗi lần chạy lại ngrok
    //private const string HostUploadFile = "https://ample-definitely-reptile.ngrok-free.app/";


    public DocumentFileController(IServiceWrapper services, IMapper mapper, IHubContext<GroupHub> groupHub)
    {
        this._service = services;
        this._mapper = mapper;
        this.groupHub = groupHub;
    }

    [HttpPost("/upload-file/{groupId}")]
    [DisableRequestSizeLimit]
    public async Task<ActionResult> UploadFile(IFormFile file, [FromRoute] int groupId, int accountId)
    {
        string httpFilePath = "";
        var documentFile = new DocumentFile();
        try
        {
            if (file.Length > 0)
            {
                var groupPath = path + "\\" + groupId;
                if (!Directory.Exists(groupPath))
                {
                    Directory.CreateDirectory(groupPath);
                }

                using (var fileStream = new FileStream(Path.Combine(groupPath, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    httpFilePath = HostUploadFile + groupId + "/" + file.FileName;
                }
            }

            if (!string.IsNullOrEmpty(httpFilePath))
            {
                documentFile.HttpLink = httpFilePath;
                documentFile.Approved = false;
                documentFile.AccountId = accountId;
                documentFile.GroupId = groupId;
                documentFile.CreatedDate = DateTime.Now;
                await _service.DocumentFiles.CreateDocumentFile(documentFile);
                await groupHub.Clients.Group(groupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("File Copy Failed", ex);
        }

        return Ok(documentFile);
    }


    [HttpGet("/get-list-file")]
    public async Task<ActionResult<DocumentFile>> GetListFile()
    {
        var result = _service.DocumentFiles.GetList();

        return Ok(result);
    }
    [HttpGet("/get-list-file-by-date")]
    public async Task<ActionResult<DocumentFile>> GetListFileByDate([FromQuery] int groupId, DateTime month)
    {
        var group = _service.Groups.GetFullByIdAsync(groupId).Result;
        if (null == group)
        {
            return NotFound();
        }

        var result = _service.DocumentFiles.GetListFileByDate(groupId, month);


        List<DocumentFileDto> resultDto = new List<DocumentFileDto>();
        foreach (var documentFile in result)
        
        {
            var dto = _mapper.Map<DocumentFileDto>(documentFile);
            resultDto.Add(dto);
        }
        return Ok(resultDto);
    }


    [HttpGet("/get-detail")]
    public async Task<ActionResult<DocumentFile>> GetFileDetail([FromQuery] int id)
    {
        var result = await _service.DocumentFiles.GetById(id);
        if (null == result)
        {
            return NotFound();
        }
        return Ok(result);
    }

 
    [HttpGet("/get-file-by-group")]
    public async Task<ActionResult<DocumentFile>> GetListFileByGroupId([FromQuery] int id)
    {
        var group = _service.Groups.GetFullByIdAsync(id).Result;
        if (null == group)
        {
            return NotFound();
        }
        var result = _service.DocumentFiles.GetListByGroupId(id);
        List<DocumentFileDto> resultDto = new List<DocumentFileDto>();
        foreach (var documentFile in result)
        {
           var dto = _mapper.Map<DocumentFileDto>(documentFile);
           resultDto.Add(dto);
        }
        return Ok(resultDto);
    }

    [HttpPut("/accept-file")]
    //[Authorize(Roles = Actor.Leader)]
    public async Task<IActionResult> UpdateFile(int id, bool approved)
    {
        var file = _service.DocumentFiles.GetById(id).Result;
        if (null == file)
        {
            return NotFound();
        }

        if (approved)
        {
            file.Approved = approved;
            await _service.DocumentFiles.UpdateDocumentFile(file);
        }

        await groupHub.Clients.Group(file.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
        return Ok("approved");
    }

    [HttpDelete("/delete-file")]
    //chỗ này thêm Authen
    //[Authorize(Roles = Actor.Leader)]
    public async Task<IActionResult> DeleteFIle(int id)
    {
        var file = _service.DocumentFiles.GetById(id).Result;
        if (null == file)
        {
            return NotFound();
        }

        await _service.DocumentFiles.DeleteDocumentFile(id);
        await groupHub.Clients.Group(file.GroupId.ToString()).SendAsync(GroupHub.OnReloadMeetingMsg);
        return Ok("Deleted");
    }
    
    [HttpGet("/get-by-accountid")]
    //chỗ này thêm Authen
    public async Task<IActionResult> GetByAccountId([FromQuery] int accountId)
    {
        var account = await _service.Accounts.GetByIdAsync(accountId);
        if (null == account)
        {
            return NotFound();
        }
        var result = _service.DocumentFiles.GetListByAccountId(accountId);
        List<DocumentFileDto> resultDto = new List<DocumentFileDto>();
        foreach (var documentFile in result)
        {
            var dto = _mapper.Map<DocumentFileDto>(documentFile);
            resultDto.Add(dto);
        }
        return Ok(resultDto);
    }
}