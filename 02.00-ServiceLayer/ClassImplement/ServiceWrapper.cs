using RepositoryLayer.Interface;
using ServiceLayer.Interface;
using ServiceLayer.Interface.Auth;
using ServiceLayer.Interface.Db;
using ServiceLayer.ClassImplement.Auth;
using Microsoft.Extensions.Configuration;
using ServiceLayer.ClassImplement.Db;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;

namespace ServiceLayer.ClassImplement
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IRepoWrapper repos;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment env;


        public ServiceWrapper(IRepoWrapper repos, IConfiguration configuration, IMapper mapper, IWebHostEnvironment env)
        {
            this.repos = repos;
            this.configuration = configuration;
            this.mapper = mapper;
            this.env = env;
            accounts = new AccountService(repos);
            auth = new AuthService(repos, configuration);
            groups = new GroupService(repos);
            classes = new ClassService(repos);
            subjects = new SubjectService(repos);
            meetings = new MeetingService(repos, mapper);
            groupMembers = new GroupMemberSerivce(repos, mapper);
            documentFiles = new DocumentFileService(repos);
            stats = new StatService(repos, mapper);
            mails = new AutoMailService(env, repos, configuration, Accounts, Stats);
        }

        private IAccountService accounts;
        public IAccountService Accounts
        {
            get
            {
                if (accounts is null)
                {
                    accounts = new AccountService(repos);
                }
                return accounts;
            }
        }

        private IAuthService auth;
        public IAuthService Auth
        {
            get
            {
                if (auth is null)
                {
                    auth = new AuthService(repos, configuration);
                }
                return auth;
            }
        }

        private IGroupService groups;
        public IGroupService Groups
        {
            get
            {
                if (groups is null)
                {
                    groups = new GroupService(repos);
                }
                return groups;
            }
        }

        private IClassService classes;
        public IClassService Classes
        {
            get
            {
                if (classes is null)
                {
                    classes = new ClassService(repos);
                }
                return classes;
            }
        }

        private ISubjectService subjects;
        public ISubjectService Subjects
        {
            get
            {
                if (subjects is null)
                {
                    subjects = new SubjectService(repos);
                }
                return subjects;
            }
        }

        private IMeetingService meetings;
        public IMeetingService Meetings
        {
            get
            {
                if (meetings is null)
                {
                    meetings = new MeetingService(repos, mapper);
                }
                return meetings;
            }
        }

        private IGroupMemberSerivce groupMembers;
        public IGroupMemberSerivce GroupMembers
        {
            get
            {
                if (groupMembers is null)
                {
                    groupMembers = new GroupMemberSerivce(repos, mapper);
                }
                return groupMembers;
            }
        }
        private IDocumentFileService documentFiles;

        public IDocumentFileService DocumentFiles
        {
            get
            {
                if (documentFiles is null)
                {
                    documentFiles = new DocumentFileService(repos);
                }
                return documentFiles;
            }
        }

        private IStatService stats;
        public IStatService Stats
        {
            get
            {
                if (stats is null)
                {
                    stats = new StatService(repos, mapper);
                }
                return stats;
            }
        }

        private IAutoMailService mails;
        public IAutoMailService Mails
        {
            get
            {
                if (mails is null)
                {
                    mails = new AutoMailService(env, repos, configuration, Accounts, Stats);
                }
                return mails;
            }
        }
    }
}

