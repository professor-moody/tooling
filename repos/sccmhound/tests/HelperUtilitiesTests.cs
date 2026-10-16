using SCCMHound.src;
using SCCMHound.src.models;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SCCMHound.tests
{
    public class HelperUtilitiesTests
    {   
        [Fact]
        public void getDomainSidFromUserSid_success()
        {
            string result = HelperUtilities.getDomainSidFromUserSid("S-1-5-21-1234567890-1234567890-123456789-515");
            Assert.Equal("S-1-5-21-1234567890-1234567890-123456789", result);
        }

        [Fact]
        public void createLookupTable_user_success()
        {
            User user1 = new User();
            user1.Properties["sccmUniqueUserName"] = "user1";

            User user2 = new User();
            user2.Properties["sccmUniqueUserName"] = "user2";

            List<User> users = new List<User> { user1, user2 };
            Dictionary<string, User> userLookupTable = HelperUtilities.createLookupTableUsers(users);
            Assert.Same(user1, userLookupTable["user1"]);
            Assert.Same(user2, userLookupTable["user2"]);
        }

        [Fact]
        public void createLookupTable_user_failure()
        {
            User user1 = new User();
            user1.Properties["sccmUniqueUserName"] = "user1";

            User user2 = new User();
            user2.Properties["sccmUniqueUserName"] = "user2";

            List<User> users = new List<User> { user1, user2 };
            Dictionary<string, User> userLookupTable = HelperUtilities.createLookupTableUsers(users);
            Assert.Same(user1, userLookupTable["user1"]);
            Assert.NotSame(user1, userLookupTable["user2"]);
        }

        [Fact]
        public void createLookupTable_computer_success()
        {
            ComputerExt computer1 = new ComputerExt();
            computer1.Properties["sccmName"] = "computer1";

            ComputerExt computer2 = new ComputerExt();
            computer2.Properties["sccmName"] = "computer2";

            List<ComputerExt> computers = new List<ComputerExt> { computer1, computer2 };
            Dictionary<string, ComputerExt> computerLookupTable = HelperUtilities.createLookupTableComputers(computers);
            Assert.Same(computer1, computerLookupTable["computer1"]);
            Assert.Same(computer2, computerLookupTable["computer2"]);
        }

        [Fact]
        public void createLookupTable_computer_failure()
        {
            ComputerExt computer1 = new ComputerExt();
            computer1.Properties["sccmName"] = "computer1";

            ComputerExt computer2 = new ComputerExt();
            computer2.Properties["sccmName"] = "computer2";

            List<ComputerExt> computers = new List<ComputerExt> { computer1, computer2 };
            Dictionary<string, ComputerExt> computerLookupTable = HelperUtilities.createLookupTableComputers(computers);
            Assert.Same(computer1, computerLookupTable["computer1"]);
            Assert.NotSame(computer2, computerLookupTable["computer1"]);
        }
    }
}
