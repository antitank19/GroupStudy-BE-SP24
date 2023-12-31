using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System;

namespace DataLayer.DBObject
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        #region Group Member
        public virtual ICollection<GroupMember> GroupMembers { get; set; } = new Collection<GroupMember>();
        #endregion
        
        #region Meeting Room
        public virtual ICollection<Meeting> Meetings { get; set; } = new Collection<Meeting>();
        #endregion

        #region Invite
        public virtual ICollection<Invite> JoinInvites { get; set; } = new Collection<Invite>();
        #endregion

        #region Request
        public virtual ICollection<Request> JoinRequests { get; set; } = new Collection<Request>();
        #endregion

        #region Class
        //Class
        [ForeignKey("ClassId")]
        public int ClassId { get; set; }
        public Class Class { get; set; }
        #endregion

        #region Subjects
        public virtual ICollection<GroupSubject> GroupSubjects { get; set; } = new Collection<GroupSubject>();
        #endregion

        #region Schedules
        public virtual ICollection<Schedule> Schedules { get; set; } = new Collection<Schedule>();
        #endregion
    }
}