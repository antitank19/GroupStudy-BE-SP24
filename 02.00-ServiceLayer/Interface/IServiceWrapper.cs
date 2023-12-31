using ServiceLayer.Interface.Auth;
using ServiceLayer.Interface.Db;

namespace ServiceLayer.Interface
{
    public interface IServiceWrapper
    {
        public IAccountService Accounts { get; }
        public IAuthService Auth { get; }
        public IGroupService Groups { get; }
        public IClassService Classes { get; }
        public ISubjectService Subjects { get; }
        public IMeetingService Meetings { get; }
        public IGroupMemberSerivce GroupMembers { get; }
        public IDocumentFileService DocumentFiles { get; }
        public IAutoMailService Mails { get; }
        public IStatService Stats { get; }

    }
}
