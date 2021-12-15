using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Projected.Data.LDAP
{
    public class CoreLogicConnectToAuthLdapBehavior : IConnectToAuthLdapBehavior
    {
        private readonly IConfiguration _configuration;
        private readonly List<IConnectToGroupLdapBehavior> _connectToGroupLdapBehaviors;
        private readonly IGetLdapGroupsBehavior _getLdapGroupsBehavior;

        public CoreLogicConnectToAuthLdapBehavior(IConfiguration configuration, List<IConnectToGroupLdapBehavior> connectToGroupLdapBehaviors, IGetLdapGroupsBehavior getLdapGroupsBehavior)
        {
            _configuration = configuration;
            _connectToGroupLdapBehaviors = connectToGroupLdapBehaviors;
            _getLdapGroupsBehavior = getLdapGroupsBehavior;
        }

        public LdapConnection Connect(string username, string password)
        {
            var coreLogicLdapConnection = new LdapConnection(new LdapDirectoryIdentifier(_configuration.GetValue<string>("CoreLogicLdapServer"), 389));
            coreLogicLdapConnection.Bind(new NetworkCredential(username, password));
            return coreLogicLdapConnection;
        }
        public IEnumerable<string> GetGroups(string username, string password)
        {
            var groups = new List<string>();

            using (var authLdapConnection = Connect(username, password))
            {
                foreach (var connectGroupBehavior in _connectToGroupLdapBehaviors)
                {
                    using (var groupLdapConnection = connectGroupBehavior.Connect())
                    {
                        groups.AddRange(_getLdapGroupsBehavior.GetGroups(authLdapConnection, groupLdapConnection, username, connectGroupBehavior.GetObjectClass()));
                    }
                }
            }

            return groups;
        }
    }
}
