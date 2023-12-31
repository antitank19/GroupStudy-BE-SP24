namespace ShareResource.DTO.File;

public class DocumentFileDto
{
    public int Id { get; set; }
    public string HttpLink { get; set; }

    public int AccountId { get; set; } 
    
    public string AccountName { get; set; }

    public Boolean Approved { get; set; }
    public DateTime CreatedDate { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }

}