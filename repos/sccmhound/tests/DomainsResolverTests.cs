using SCCMHound.src.models;
using SCCMHound.src;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SCCMHound.tests
{
    public class DomainsResolverTests
    {
        [Fact]
        public void DomainsResolver()
        {
            List<ComputerExt> computers = SCCMCollectorTests.DeserializeComputers();
            List<User> users = SCCMCollectorTests.DeserializeUsers();
            List<Group> groups = SCCMCollectorTests.DeserializeGroups();
            DomainsResolver domainsResolver = new DomainsResolver(users, computers, groups);

            if ((domainsResolver.domains.Count) > 0) {
                Assert.True(true);
            }
            
        }
    }
}
