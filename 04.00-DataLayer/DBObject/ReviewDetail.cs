using ShareResource.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.DBObject
{
    public class ReviewDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Comment { get; set; }
        public ReviewResultEnum Result { get; set; }

        #region   Review
        public int ReviewId { get; set; }
        [ForeignKey(nameof(ReviewId))]
        public Review Review { get; set; }
        #endregion

        #region  Reviewer
        public int? ReviewerId { get; set; }
        [ForeignKey(nameof(ReviewerId))]
        public Account? Reviewer { get; set; }
        #endregion

    }

    
}