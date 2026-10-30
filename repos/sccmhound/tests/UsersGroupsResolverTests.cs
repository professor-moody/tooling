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
    public class UsersGroupsResolverTests
    {
        [Fact]
        public void UsersGroupsResolver_test()
        {
            List<User> users = SCCMCollectorTests.DeserializeUsers();
            List<Group> groups = SCCMCollectorTests.DeserializeGroups();
            UsersGroupsResolver usersGroupsResolver = new UsersGroupsResolver(users, groups);

            Assert.True(true);
        }
    }
}
