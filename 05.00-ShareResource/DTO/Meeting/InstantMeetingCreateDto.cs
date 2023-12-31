using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class InstantMeetingCreateDto
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set { content = value.Trim(); }
        }
        public int GroupId { get; set; }
    }
}
