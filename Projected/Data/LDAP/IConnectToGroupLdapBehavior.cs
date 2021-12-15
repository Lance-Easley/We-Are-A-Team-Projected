using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.LDAP
{
    public interface IConnectToGroupLdapBehavior
    {
        LdapConnection Connect();
        string GetObjectClass();
    }
}
