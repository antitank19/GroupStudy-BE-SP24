using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.DBObject
{
    public class Chat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Time { get; set; }

        #region Account
        //Account
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        #endregion

        #region Meeting
        //Account
        [ForeignKey("MeetingId")]
        public int MeetingId { get; set; }
        public Meeting Meeting { get; set; }
        #endregion
    }
}