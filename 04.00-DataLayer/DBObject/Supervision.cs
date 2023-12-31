using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareResource.Enums;

namespace DataLayer.DBObject
{
    public class Supervision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public RequestStateEnum State { get; set; } = RequestStateEnum.Waiting;

        //Student
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual Account Student { get; set; }

        //Student
        public int ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Account Parent { get; set; }

    }
}
