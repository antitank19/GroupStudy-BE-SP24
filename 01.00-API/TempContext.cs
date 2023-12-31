using DataLayer.DBContext;
using DataLayer.DBObject;
using Microsoft.EntityFrameworkCore;

namespace API
{
    /// <summary>
    /// Used for scaffolding api controllers. Delete later
    /// </summary>
    public class TempContext : DbContext
    {
        public TempContext(DbContextOptions<TempContext> options) : base(options)
        {

        }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<GroupSubject> GroupSubjects { get; set; }
        public virtual DbSet<Meeting> Meetings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<ReviewDetail> ReviewDetails { get; set; }

        public virtual DbSet<DocumentFile> DocumentFiles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Account>().HasData(
        //        new Account
        //        {
        //            Id = 1,
        //            Email = "user1",
        //            Password = "123456789"
        //        },
        //        new Account
        //        {
        //            Id = 2,
        //            Email = "user2",
        //            Password = "123456789"
        //        },
        //        new Account
        //        {
        //            Id = 3,
        //            Email = "user3",
        //            Password = "123456789"
        //        },
        //        new Account
        //        {
        //            Id = 4,
        //            Email = "user4",
        //            Password = "123456789"
        //        }
        //    );
        //}

        //public DbSet<DataLayer.DBObject.Account>? User { get; set; }
    }
}
