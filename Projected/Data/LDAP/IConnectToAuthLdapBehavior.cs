using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.LDAP
{
    public interface IConnectToAuthLdapBehavior
    {
        LdapConnection Connect(string username, string password);
        IEnumerable<string> GetGroups(string username, string password);
    }
}
