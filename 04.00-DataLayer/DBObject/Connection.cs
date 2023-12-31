using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataLayer.DBObject
{
    [Table("MeetingParticipations")]
    public class Connection
    {
        //public Connection(string connectionId, string userName)
        //{
        //    Id = connectionId;
        //    UserName = userName;
        //}
        [Key]
        public string Id { get; set; }   
        public DateTime Start { get;set; }
        public DateTime? End { get;set; }

        #region Meeting
        [ForeignKey("MeetingId")]
        public int MeetingId { get; set; }
        public virtual Meeting Meeting { get; set; }
        #endregion


        #region Student
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public string UserName { get; set; }

        public virtual Account Account { get; set; }
        #endregion
    }
}