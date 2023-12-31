using DataLayer.DBObject;
using ShareResource.Enums;
using ShareResource.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DbSeeding
{
    public static class DbSeed
    {
        public static Role[] Roles = new Role[] {
            new Role
            {
                Id = 1,
                Name = "Parent"
            },
            new Role
            {
                Id = 2,
                Name = "Student"
            }};
        public static Account[] Accounts = new Account[] {
            new Account
            {
                Id = 1,
                Username = "student1",
                FullName = "Trần Khải Minh Khôi",
                Email = "trankhaiminhkhoi10a3@gmail.com",
                //Password = StringUtils.CustomHash("123456789"),
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                ClassId = 12,
                DateOfBirth = new DateTime(2009,5, 5),
                RoleId = 2
            },
            new Account
            {
                Id = 2,
                Username = "student2",
                FullName = "Đào Thị Bưởi",
                Email = "student2@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 12,
                RoleId = 2
            },
            new Account
            {
                Id = 3,
                Username = "student3",
                FullName = "Trần Văn Chình",
                Email = "student3@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 11,
                RoleId = 2
            },
            new Account
            {
                Id = 4,
                Username = "student4",
                FullName = "Lí Thị Diệu",
                Email = "student4@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 11,
                RoleId = 2
            },
            new Account
            {
                Id = 5,
                Username = "student5",
                FullName = "Trần Văn Em",
                Email = "student5@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 10,
                RoleId = 2
            },
            new Account
            {
                Id = 6,
                Username = "student6",
                FullName = "Lí Chính Phúc",
                Email = "student6@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 12,
                RoleId = 2
            },
            new Account
            {
                Id = 7,
                Username = "student7",
                FullName = "Ngô Văn Gia",
                Email = "student7@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 12,
                RoleId = 2
            },
            new Account
            {
                Id = 8,
                Username = "student8",
                FullName = "Trần Văn Hơn",
                Email = "student8@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 10,
                RoleId = 2
            }
            , new Account
            {
                Id = 9,
                Username = "student9",
                FullName = "Trần Văn Yến",
                Email = "student9@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 11,
                RoleId = 2
            }, new Account
            {
                Id = 10,
                Username = "student10",
                FullName = "Trần Văn Dền",
                Email = "student10@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                Schhool="THCS Minh Đức",
                DateOfBirth = new DateTime(2009,5, 5),
                ClassId = 11,
                RoleId = 2
            },
            new Account
            {
                Id = 11,
                Username = "parent1",
                FullName = "Trần Ba",
                Email = "trankhaiminhkhoi@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                DateOfBirth = new DateTime(1975,5, 5),
                RoleId = 1
            }  ,
            new Account
            {
                Id = 12,
                Username = "parent2",
                FullName = "Trần Văn Mạ",
                Email = "parent2@gmail.com",
                Password = StringUtils.CustomHash("123456789"),
                Phone = "0123456789",
                DateOfBirth = new DateTime(1975,5, 5),
                RoleId = 1
            }
        };
        public static Supervision[] Supervisions = new Supervision[]
        {
            new Supervision
            {
                Id = 1,
                StudentId=1,
                ParentId=11,
                State=RequestStateEnum.Approved
            },
            new Supervision
            {
                Id = 2,
                StudentId=2,
                ParentId=11,
                State=RequestStateEnum.Approved
            },
            new Supervision
            {
                Id = 3,
                StudentId=1,
                ParentId=12,
                State=RequestStateEnum.Waiting
            },
            new Supervision
            {
                Id = 4,
                StudentId=2,
                ParentId=12,
                State=RequestStateEnum.Waiting
            },
            new Supervision
            {
                Id = 5,
                StudentId=3,
                ParentId=12,
                State=RequestStateEnum.Waiting
            },
            new Supervision
            {
                Id = 6,
                StudentId=4,
                ParentId=12,
                State=RequestStateEnum.Waiting
            },
        };
        public static Class[] Classes = new Class[]
        {
            new Class
            {
                Id = 6,
                Name = 6
            },
            new Class
            {
                Id = 7,
                Name = 7
            },
            new Class
            {
                Id = 8,
                Name = 8
            },
            new Class
            {
                Id = 9,
                Name = 9
            },
            new Class
            {
                Id = 10,
                Name = 10
            },
            new Class
            {
                Id = 11,
                Name = 11
            },
            new Class
            {
                Id = 12,
                Name = 12
            }
        };
        public static Subject[] Subjects = new Subject[]
        {
            new Subject
            {
                Id = 1,
                Name = "Toán"
            },
            new Subject
            {
                Id = 2,
                Name = "Lí"
            },
            new Subject
            {
                Id = 3,
                Name = "Hóa"
            },
            new Subject
            {
                Id = 4,
                Name = "Văn"
            },
            new Subject
            {
                Id = 5,
                Name = "Sử"
            },
            new Subject
            {
                Id = 6,
                Name = "Địa"
            },
            new Subject
            {
                Id = 7,
                Name = "Sinh"
            },
            new Subject
            {
                Id = 8,
                Name = "Anh"
            }
        };
        public static Group[] Groups = new Group[]
        {
            new Group
            {
                Id = 1,
                Name = "Ôn thi đại học",
                ClassId = 12,
            },
            new Group
            {
                Id = 2,
                Name = "Khối A1",
                ClassId = 11,
            } ,
            new Group
            {
                Id = 3,
                Name = "Lớp 12A4",
                ClassId = 12,
            } ,
            new Group
            {
                Id = 4,
                Name = "Chuẩn bị thi ĐH",
                ClassId = 12,
            },
            new Group
            {
                Id = 5,
                Name = "AE 12A1",
                ClassId = 12,
            },
            new Group
            {
                Id = 6,
                Name = "10A4",
                ClassId = 10,
            },
        };
        public static GroupMember[] GroupMembers = new GroupMember[]
        {
                        #region Member Group 1
                        new GroupMember
                        {
                            Id = 1,
                            GroupId = 1,
                            AccountId = 1,
                            MemberRole = GroupMemberRole.Leader,
                            IsActive = true,
                        },
                        new GroupMember
                        {
                            Id = 2,
                            GroupId = 1,
                            AccountId = 2,
                            MemberRole = GroupMemberRole.Member,
                            IsActive = true,
                            //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        },
                        //Fix later
                        //new GroupMember
                        //{
                        //    Id = 3,
                        //    GroupId = 1,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 4,
                        //    GroupId = 1,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        new GroupMember
                        {
                            //Id = 5,
                            Id = 3,
                            GroupId = 1,
                            AccountId = 5,
                            MemberRole = GroupMemberRole.Member,
                            IsActive = true,
                            //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        },
                        #endregion
                        #region Member group 2
                        new GroupMember
                        {
                            //Id = 6,
                            Id = 4,
                            GroupId = 2,
                            AccountId = 1,
                            MemberRole = GroupMemberRole.Leader,
                            IsActive = true,
                        },
                        new GroupMember
                        {
                            //Id = 7,
                            Id = 5,
                            GroupId = 2,
                            AccountId = 2,
                            MemberRole = GroupMemberRole.Member,
                            IsActive = true,
                            ////InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        },
                        //new GroupMember
                        //{
                        //    Id = 8,
                        //    GroupId = 2,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Requesting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 9,
                        //    GroupId = 2,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Inviting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion   
                        #region Member group 3
                        new GroupMember
                        {
                            //Id = 10,
                            Id = 6,
                            GroupId = 3,
                            AccountId = 2,
                            MemberRole = GroupMemberRole.Leader,
                            IsActive = true,
                        },
                        new GroupMember
                        {
                            //Id = 11,
                            Id = 7,
                            GroupId = 3,
                            AccountId = 1,
                            MemberRole = GroupMemberRole.Member,
                            IsActive = true,
                            //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        },
                        //new GroupMember
                        //{
                        //    Id = 12,
                        //    GroupId = 3,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 13,
                        //    GroupId = 3,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                        #region member group 4
                        new GroupMember
                        {
                            //Id = 14,
                            Id = 8,
                            GroupId = 4,
                            AccountId = 2,
                            MemberRole = GroupMemberRole.Leader,
                            IsActive = true,
                        },
                        // new GroupMember
                        //{
                        //    Id = 15,
                        //    GroupId = 4,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 16,
                        //    GroupId = 4,
                        //    AccountId = 4,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                        #region member group 5
                        new GroupMember
                        {
                            //Id = 17,
                            Id = 9,
                            GroupId = 5,
                            AccountId = 3,
                            MemberRole = GroupMemberRole.Leader,
                            IsActive = true,
                        },
                        // new GroupMember
                        //{
                        //    Id = 18,
                        //    GroupId = 5,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 19,
                        //    GroupId = 5,
                        //    AccountId = 3,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
                        #region member group 6
                        new GroupMember
                        {
                            //Id = 20,
                            Id = 10,
                            GroupId = 6,
                            AccountId = 3,
                            MemberRole = GroupMemberRole.Leader,
                            IsActive = true,
                        }
                        // new GroupMember
                        //{
                        //    Id = 21,
                        //    GroupId = 6,
                        //    AccountId = 2,
                        //    State = GroupMemberState.Inviting,
                        //    //InviteMessage = "Nhóm của mình rất hay. Bạn vô nha"
                        //},
                        //new GroupMember
                        //{
                        //    Id = 22,
                        //    GroupId = 6,
                        //    AccountId = 1,
                        //    State = GroupMemberState.Requesting,
                        //    //RequestMessage = "Nhóm của bạn rất hay. Bạn cho mình vô nha"
                        //},
                        #endregion
        };
        public static GroupSubject[] GroupSubjects = new GroupSubject[]
        {
            #region Subject Group 1
            new GroupSubject
            {
                Id = 1,
                GroupId = 1,
                SubjectId = (int)SubjectEnum.Toan
            },
            new GroupSubject
            {
                Id = 2,
                GroupId = 1,
                SubjectId = (int)SubjectEnum.Van
            },
            new GroupSubject
            {
                Id = 3,
                GroupId = 1,
                SubjectId = (int)SubjectEnum.Anh
            },
            #endregion
            #region Subject group 2
            new GroupSubject
            {
                Id = 4,
                GroupId = 2,
                SubjectId = (int)SubjectEnum.Toan
            },
            new GroupSubject
            {
                Id = 5,
                GroupId = 2,
                SubjectId = (int)SubjectEnum.Li
            },
            new GroupSubject
            {
                Id = 6,
                GroupId = 2,
                SubjectId = (int)SubjectEnum.Hoa
            } ,
            #endregion
            #region Subject group 3
            new GroupSubject
            {
                Id = 7,
                GroupId = 3,
                SubjectId = (int)SubjectEnum.Su
            },
            new GroupSubject
            {
                Id = 8,
                GroupId = 3,
                SubjectId = (int)SubjectEnum.Dia
            },
            #endregion
            #region Subject group 4
            new GroupSubject
            {
                Id = 9,
                GroupId = 4,
                SubjectId = (int)SubjectEnum.Su
            },
            #endregion
            #region Subject group 5
            new GroupSubject
            {
                Id = 10,
                GroupId = 5,
                SubjectId = (int)SubjectEnum.Su
            },
            #endregion
            #region Subject group 6
            new GroupSubject
            {
                Id = 11,
                GroupId = 6,
                SubjectId = (int)SubjectEnum.Su
            },
            #endregion
            new GroupSubject
            {
                Id = 12,
                GroupId = 1,
                SubjectId = (int)SubjectEnum.Li
            },
        };
        public static Invite[] Invites = new Invite[]
        {
            #region Group 1
            new Invite {
                Id = 1,
                GroupId= 1,
                AccountId=2,
                State = RequestStateEnum.Approved
            },
            new Invite
            {
                Id = 2,
                GroupId = 1,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion

            #region Group 2
                new Invite
            {
                Id = 3,
                GroupId = 2,
                AccountId = 2,
                State = RequestStateEnum.Approved,
            },
            new Invite
            {
                Id = 4,
                GroupId = 2,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion 

            #region Group 3
            new Invite
            {
                Id = 5,
                GroupId = 3,
                AccountId = 1,
                State = RequestStateEnum.Approved,
            },
            new Invite
            {
                Id = 6,
                GroupId = 3,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 4
            new Invite
            {
                Id = 7,
                GroupId = 4,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 5
            new Invite
            {
                Id = 8,
                GroupId = 5,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 6
            new Invite
            {
                Id = 9,
                GroupId = 6,
                AccountId = 2,
                State = RequestStateEnum.Waiting,
            },
            new Invite
            {
                Id = 10,
                GroupId = 6,
                AccountId = 1,
                State = RequestStateEnum.Waiting,
            },
            #endregion

        };
        public static Request[] Requests = new Request[]
        {
                        
            #region Group 1
            new Request
            {
                Id = 1,
                GroupId = 1,
                AccountId = 4,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 2
            new Request
            {
                Id = 2,
                GroupId = 2,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 3
                new Request
            {
                Id = 3,
                GroupId = 3,
                AccountId = 4,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 4
            new Request
            {
                Id = 4,
                GroupId = 4,
                AccountId = 4,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 5
            new Request
            {
                Id = 5,
                GroupId = 5,
                AccountId = 3,
                State = RequestStateEnum.Waiting,
            },
            #endregion
                        
            #region Group 6
            new Request
            {
                Id = 6,
                GroupId = 6,
                AccountId = 1,
                State = RequestStateEnum.Waiting,
            },
            #endregion
        };
        public static Meeting[] Meetings = new Meeting[]
        {
            #region meeting for group 1
            //Forgoten meeting
            new Meeting
            {
                Id = 1,
                GroupId=1,
                Name=$"Ôn tập kiểm tra 15p",
                Content=$"Ôn tập kiểm tra 15p {DateTime.Now.AddDays(-3).ToShortDateString()}",
                ScheduleStart = DateTime.Now.AddDays(-3),
                ScheduleEnd = DateTime.Now.AddDays(-3).AddHours(1),
            },
            //Ended schedule meeting
            new Meeting
            {
                Id = 2,
                GroupId=1,
                Name=$"Ôn tập kiểm tra 1 tiết",
                Content=$"Ôn tập kiểm tra 1 tiết {DateTime.Now.AddMonths(-2).AddDays(-2).ToShortDateString()}",
                ScheduleStart = DateTime.Now.AddMonths(-2).AddDays(-2).AddMinutes(15),
                ScheduleEnd = DateTime.Now.AddMonths(-2).AddDays(-2).AddHours(1),
                Start = DateTime.Now.AddMonths(-2).AddDays(-2).AddMinutes(30),
                End = DateTime.Now.AddMonths(-2).AddDays(-2).AddHours(2),
                CountMember = 1,
            },
            //Ended instant meeting
            new Meeting
            {
                Id = 3,
                GroupId=1,
                Name=$"Ôn tập thi toán",
                Content=$"Ôn tập thi toán {DateTime.Now.AddDays(-2).ToShortDateString()}",
                Start = DateTime.Now.AddDays(-2).AddMinutes(30),
                End = DateTime.Now.AddDays(-2).AddHours(2),
                CountMember = 1,
            },
            //Live schedule meeting
            new Meeting
            {
                Id = 4,
                GroupId=1,
                Name=$"Ôn tập kiểm tra lí",
                Content=$"Ôn tập kiểm tra lí {DateTime.Now.ToShortDateString()}",
                ScheduleStart = DateTime.Now.AddMinutes(15),
                ScheduleEnd = DateTime.Now.AddHours(1),
                Start = DateTime.Now.AddMinutes(30),
            },
            //Live Instant meeting
            new Meeting
            {
                Id = 5,
                GroupId=1,
                Name=$"Ôn tập kiểm tra",
                Content=$"Ôn tập kiểm tra {DateTime.Now.ToShortDateString()}",
                Start = DateTime.Now.AddMinutes(-30),
            },
            //Future Schedule meeting
            new Meeting
            {
                Id = 6,
                GroupId=1,
                Name=$"Ôn tập thi toán",
                Content=$"Ôn tập thi toán {DateTime.Now.ToShortDateString()}",
                ScheduleStart = DateTime.Now.AddMinutes(-15),
                ScheduleEnd = DateTime.Now.AddHours(1),
            },
            new Meeting
            {
                Id = 7,
                GroupId=1,
                Name=$"Ôn tập thi lí",
                Content=$"Ôn tập thi lí {DateTime.Now.ToShortDateString()}",
                ScheduleStart = DateTime.Now.AddMinutes(15),
                ScheduleEnd = DateTime.Now.AddHours(1),
            },
            new Meeting
            {
                Id = 8,
                GroupId=1,
                Name=$"Ôn tập thi Toán",
                Content=$"Ôn tập thi Toán {DateTime.Now.AddDays(1).ToShortDateString()}",
                ScheduleStart = DateTime.Now.AddDays(1).AddMinutes(15),
                ScheduleEnd = DateTime.Now.AddDays(1).AddHours(1),
            },
            #endregion
        };
        public static Connection[] Connections = new Connection[]
        {
            #region Meeting 2
            new Connection
            {
                Id= "Id1",
                AccountId = 1,
                MeetingId = 2,
                Start = DateTime.Now.AddDays(-2).AddMinutes(30),
                End = DateTime.Now.AddDays(-2).AddMinutes(45),
                UserName = "student1",
            },
            new Connection
            {
                Id= "Id2",
                AccountId = 1,
                MeetingId = 2,
                Start = DateTime.Now.AddDays(-2).AddHours(1),
                End = DateTime.Now.AddDays(-2).AddHours(2),
                UserName = "student1",
            },
             new Connection
            {
                Id= "Id3",
                AccountId = 1,
                MeetingId = 3,
                Start = DateTime.Now.AddMonths(-2).AddDays(-2).AddHours(1),
                End = DateTime.Now.AddMonths(-2).AddDays(-2).AddHours(2),
                UserName = "student1",
            },

            #endregion
        };
        public static Review[] Reviews = new Review[]
        {
            new Review
            {
                Id = 1,
                MeetingId=2,
                RevieweeId=1,
            } ,
             new Review
            {
                Id = 2,
                MeetingId=2,
                RevieweeId=1,
            }
        };
        public static ReviewDetail[] ReviewDetails = new ReviewDetail[]
        {
            new ReviewDetail
            {
                Id=1,
                ReviewId=1,
                ReviewerId=1,
                Result=ReviewResultEnum.VeryGood,
                Comment="Bạn thuộc bài rất kĩ"
            }    ,
            new ReviewDetail
            {
                Id=2,
                ReviewId=2,
                ReviewerId=1,
                Result=ReviewResultEnum.Good,
                Comment="Bạn thuộc bài khá"
            }
        };
        public static Chat[] Chats = new Chat[]
        {
            new Chat
            {
                Id=1,
                Content="Xin lỗi mình vô trễ",
                MeetingId=2,
                AccountId=1,
                Time = DateTime.Now.AddDays(-2).AddHours(1).AddMinutes(1),
            } ,
            new Chat
            {
                Id=2,
                Content="Mình mới vào",
                MeetingId=2,
                AccountId=1,
                Time = DateTime.Now.AddDays(-2).AddHours(1).AddMinutes(1).AddSeconds(10),
            }

        };
    }
}
