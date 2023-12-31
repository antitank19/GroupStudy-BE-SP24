using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class MessageSignalrCreateDto
    {
        private string content;

        public string Content
        {
            get { return content.Trim(); }
            set { content = value.Trim(); }
        }

        public DateTime? TimeSent { get; set; } = DateTime.Now;
    }
}
