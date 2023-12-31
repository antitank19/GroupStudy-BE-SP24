using DataLayer.DBObject;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIExtension.Validator
{
    public interface IValidatorWrapper
    {
        IMeetingValidator Meetings { get; }
        IGroupValidator Groups { get; }
        IGroupMemberValidator GroupMembers { get; }
        IAccountValidator Accounts { get; }
    }
    public class ValidatorWrapper : IValidatorWrapper
    {
        private IServiceWrapper services;

        public ValidatorWrapper(IServiceWrapper services)
        {
            this.services = services;
            meetings = new MeetingValidator(services);
            groupMembers = new GroupMemberValidator(services);
            groups = new GroupValidator(services);
        }

        private IMeetingValidator meetings;

        public IMeetingValidator Meetings
        {
            get
            {
                if (meetings == null)
                {
                    meetings = new MeetingValidator(services);
                }
                return meetings;
            }
        }

        private IGroupMemberValidator groupMembers;
        public IGroupMemberValidator GroupMembers
        {
            get
            {
                if (groupMembers == null)
                {
                    groupMembers = new GroupMemberValidator(services);
                }
                return groupMembers;
            }
        }

        private IAccountValidator accounts;
        public IAccountValidator Accounts
        {
            get
            {
                if (accounts == null)
                {
                    accounts = new AccountValidator(services);
                }
                return accounts;
            }
        }

        private IGroupValidator groups;
        public IGroupValidator Groups
        {
            get
            {
                if (groups == null)
                {
                    groups = new GroupValidator(services);
                }
                return groups;
            }
        }
    }
}
