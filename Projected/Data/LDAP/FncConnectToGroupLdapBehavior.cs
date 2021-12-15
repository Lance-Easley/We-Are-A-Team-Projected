using Microsoft.Extensions.Configuration;
using System.DirectoryServices.Protocols;

namespace Projected.Data.LDAP
{
    public class FncConnectToGroupLdapBehavior : IConnectToGroupLdapBehavior
    {
        private readonly IConfiguration _configuration;

        public FncConnectToGroupLdapBehavior(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public LdapConnection Connect()
        {
            var fncLdapServer = new LdapConnection(new LdapDirectoryIdentifier(_configuration.GetValue<string>("FncLdapServer"), 389));
            fncLdapServer.Bind();
            return fncLdapServer;
        }

        public string GetObjectClass()
        {
            return "foreignSecurityPrincipal";
        }
    }
}
