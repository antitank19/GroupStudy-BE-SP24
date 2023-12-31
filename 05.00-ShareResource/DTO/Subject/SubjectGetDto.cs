using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class SubjectGetDto: BaseGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
