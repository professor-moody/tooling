using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SCCMHound.tests
{
    public class AdminServiceConnectorTests
    {
        [Fact]
        public void connect_success()
        {
            AdminServiceConnector adminServiceConnector = AdminServiceConnector.CreateInstance(TestConstants.testInstanceIP, null);
            Assert.NotNull(adminServiceConnector);
        }

        [Fact]
        public void connect_failure()
        {
            AdminServiceConnector adminServiceConnector = AdminServiceConnector.CreateInstance(TestConstants.testUnroutableIP, null);
            Assert.Null(adminServiceConnector);

        }
    }
}
