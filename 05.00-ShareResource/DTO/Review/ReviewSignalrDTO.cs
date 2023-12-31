using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class ReviewSignalrDTO
    {
        public int Id { get; set; }
        public int MeetingId { get; set; }
        public int RevieweeId { get; set; }
        public string RevieweeUsername { get; set; }

        public double Average { get; set; }
        public IList<int?> ReviewerIds { get; set; } = new List<int?>();
        public IList<string> ReviewerUsernames { get; set; } = new List<string>();
        public virtual ICollection<ReviewDetailSignalrGetDto> Details { get; set; } = new Collection<ReviewDetailSignalrGetDto>();
    }
}
