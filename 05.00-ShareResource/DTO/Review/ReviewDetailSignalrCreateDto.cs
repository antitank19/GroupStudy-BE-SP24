using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class ReviewDetailSignalrCreateDto  : BaseCreateDto
    {
        public int ReviewId { get; set; }
        public string Comment { get; set; }
        public ReviewResultEnum Result { get; set; }
    }
}
