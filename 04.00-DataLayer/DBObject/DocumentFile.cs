using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBObject;

public class DocumentFile
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string HttpLink { get; set; }

    [ForeignKey("AccountId")]
    public int AccountId { get; set; } 
    public Account Account { get; set; }

    public Boolean Approved { get; set; }
    public DateTime CreatedDate { get; set; }
    #region Group
    public int GroupId { get; set; }
    public Group Group { get; set; }
    #endregion
}