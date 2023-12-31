using AutoMapper;
using DataLayer.DBContext;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ClassImplement
{
    public class RepoWrapper : IRepoWrapper
    {
        private readonly IMapper mapper;
        private readonly GroupStudyContext dbContext;

        public RepoWrapper(GroupStudyContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            users = new AccountRepo(dbContext, mapper);
            meeting = new MeetingRepository(dbContext, mapper);
            groupMembers = new GroupMemberReposity(dbContext);
            classes = new ClassRepository(dbContext);
            subjects = new SubjectRepository(dbContext);
            connections = new ConnectionRepository(dbContext);
            reviews = new ReviewRepository(dbContext);
            reviewDetails = new ReviewDetailRepository(dbContext);
            schedules = new ScheduleRepository(dbContext);
            supervisions = new SupervisionRepository(dbContext);
            chats = new ChatRepository(dbContext);
        }

        private IAccountRepo users;
        public IAccountRepo Accounts
        {
            get
            {
                if (users is null)
                {
                    users = new AccountRepo(dbContext, mapper);
                }
                return users;
            }
        }

        //private IMeetingRepository meetingRooms;
        //public IMeetingRepository Meetings
        //{
        //    get
        //    {
        //        if (meetingRooms is null)
        //        {
        //            meetingRooms = new MeetingRepository(dbContext, mapper);
        //        }
        //        return meetingRooms;
        //    }
        //}

        private IMeetingRepository meeting;
        public IMeetingRepository Meetings
        {
            get
            {
                if (meeting is null)
                {
                    meeting = new MeetingRepository(dbContext, mapper);
                }
                return meeting;
            }
        }

        private IGroupRepository groups;
        public IGroupRepository Groups
        {
            get
            {
                if (groups is null)
                {
                    groups = new GroupRepository(dbContext);
                }
                return groups;
            }
        }

        private IGroupMemberReposity groupMembers;
        public IGroupMemberReposity GroupMembers
        {
            get
            {
                if (groupMembers is null)
                {
                    groupMembers = new GroupMemberReposity(dbContext);
                }
                return groupMembers;
            }
        }

        private IClassReposity classes;
        public IClassReposity Classes
        {
            get
            {
                if (classes is null)
                {
                    classes = new ClassRepository(dbContext);
                }
                return classes;
            }
        }

        private ISubjectRepository subjects;
        public ISubjectRepository Subjects
        {
            get
            {
                if (subjects is null)
                {
                    subjects = new SubjectRepository(dbContext);
                }
                return subjects;
            }
        }

        private IConnectionRepository connections;
        public IConnectionRepository Connections
        {
            get
            {
                if (connections is null)
                {
                    connections = new ConnectionRepository(dbContext);
                }
                return connections;
            }
        }

        private IDocumentFileRepository documents;
        public IDocumentFileRepository DocumentFiles
        {
            get
            {
                if (documents is null)
                {
                    documents = new DocumentFileRepository(dbContext);
                }
                return documents;
            }

        }

        private IReviewRepository reviews;
        public IReviewRepository Reviews
        {
            get
            {
                if (reviews is null)
                {
                    reviews = new ReviewRepository(dbContext);
                }
                return reviews;
            }

        }

        private IReviewDetailRepository reviewDetails;
        public IReviewDetailRepository ReviewDetails
        {
            get
            {
                if (reviewDetails is null)
                {
                    reviewDetails = new ReviewDetailRepository(dbContext);
                }
                return reviewDetails;
            }

        }

        public IScheduleRepository schedules;
        public IScheduleRepository Schedules
        {
            get
            {
                if (schedules is null)
                {
                    schedules = new ScheduleRepository(dbContext);
                }
                return schedules;
            }

        }

        private IInviteReposity invites;
        public IInviteReposity Invites
        {
            get
            {
                if (invites is null)
                {
                    invites = new InviteReposity(dbContext);
                }
                return invites;
            }

        }

        private IRequestReposity requests;
        public IRequestReposity Requests
        {
            get
            {
                if (requests is null)
                {
                    requests = new RequestReposity(dbContext);
                }
                return requests;
            }

        }

        private ISupervisionRepository supervisions;
        public ISupervisionRepository Supervisions
        {
            get
            {
                if (supervisions is null)
                {
                    supervisions = new SupervisionRepository(dbContext);
                }
                return supervisions;
            }

        }

        private IChatRepository chats;
        public IChatRepository Chats
        {
            get
            {
                if (chats is null)
                {
                    chats = new ChatRepository(dbContext);
                }
                return chats;
            }

        }
    }
}
