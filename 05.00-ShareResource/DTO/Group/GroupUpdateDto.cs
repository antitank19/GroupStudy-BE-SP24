using ShareResource.Enums;

namespace ShareResource.DTO
{
    public class GroupUpdateDto : BaseUpdateDto
    {
        public int Id { get; set; }
        private string? name;

        public string? Name
        {
            get { return name; }
            set { name = value.Trim(); }
        }

        public int? ClassId { get; set; }
        public virtual ICollection<SubjectEnum>? SubjectIds { get; set; }
    }
}
