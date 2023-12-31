using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBObject
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        #region Reviewee
        public int RevieweeId { get; set; }
        [ForeignKey(nameof(RevieweeId))]
        public Account Reviewee { get; set; }
        #endregion

        #region Meeting
        public int MeetingId { get; set; }
        [ForeignKey(nameof(MeetingId))]
        public Meeting Meeting { get; set; }
        #endregion

        #region ReviewDetails
        public virtual ICollection<ReviewDetail> Details { get; set; } = new Collection<ReviewDetail>();
        #endregion
    }

}