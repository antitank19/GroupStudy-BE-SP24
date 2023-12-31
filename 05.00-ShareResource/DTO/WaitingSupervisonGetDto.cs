using ShareResource.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareResource.DTO
{
    public class WaitingSupervisonGetDto
    {
        public int Id { get; set; }
        public RequestStateEnum State { get; set; }
        public int ParentId { get; set; }
        public string ParentUserName { get; set; }
        public string ParentFullName { get; set; }
        public string ParentEmail { get; set; }
        public DateTime ParentDateOfBirth { get; set; }

        public int StudentId { get; set; }
        public string StudentUserName { get; set; }
        public string StudentFullName { get; set; }
        public string StudentSchool { get; set; }
        public string StudentEmail { get; set; }
        public DateTime StudentDateOfBirth { get; set; }
    }
}
