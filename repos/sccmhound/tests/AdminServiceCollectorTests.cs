using Newtonsoft.Json.Linq;
using ProtoBuf.Meta;
using SCCMHound.src;
using SCCMHound.src.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static SCCMHound.tests.SCCMCollectorTests;

namespace SCCMHound.tests
{
    public class AdminServiceCollectorTests
    {
        public static string testInstanceIP = TestConstants.testInstanceIP;
        public static string testInstanceSiteCode = TestConstants.testInstanceSiteCode;

        [Fact]
        public void SubmitCMPivotQuery_success()
        {
            AdminServiceConnector connector = AdminServiceConnector.CreateInstance(testInstanceIP, null);
            AdminServiceCollector collector = new AdminServiceCollector(connector);
            Assert.True(collector.SubmitCMPivotQuery("Administrators") > 0);
        }

        [Fact]
        public void QueryCollections_success()
        {
            AdminServiceConnector connector = AdminServiceConnector.CreateInstance(testInstanceIP, null);
            AdminServiceCollector collector = new AdminServiceCollector(connector);
            collector.GetCollections();
        }

        [Fact]
        public void GetAdministrators_success()
        {
            AdminServiceConnector connector = AdminServiceConnector.CreateInstance(testInstanceIP, null);
            AdminServiceCollector collector = new AdminServiceCollector(connector);
            List<LocalAdmin> localAdmins = collector.GetAdministrators();
            Assert.True(localAdmins.Count() > 0);
        }

        [Fact]
        public void SerializeLocalAdmins()
        {
            AdminServiceConnector connector = AdminServiceConnector.CreateInstance(testInstanceIP, null);
            AdminServiceCollector collector = new AdminServiceCollector(connector);
            List<LocalAdmin> localAdmins = collector.GetAdministrators();
            
            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<LocalAdmin>));

            using (var output = File.Create("localAdmins.bin"))
            {
                rttModel.Serialize(output, localAdmins);
            }
            Debug.Print("Serialized localAdmins to localAdmins.bin");
        }

        
        public static List<LocalAdmin> DeserializeLocalAdmins()
        {

            var rttModel = RuntimeTypeModel.Default;
            List<LocalAdmin> localAdmins;

            using (var input = File.OpenRead("localAdmins.bin"))
            {
                localAdmins = (List<LocalAdmin>)rttModel.Deserialize(input, null, typeof(List<LocalAdmin>));
                return localAdmins;

            }
        }

        [Fact]
        public void SerializeRelationshipsCMPivot()
        {
            AdminServiceConnector connector = AdminServiceConnector.CreateInstance(testInstanceIP, null);
            AdminServiceCollector collector = new AdminServiceCollector(connector);
            List<UserMachineRelationship> relationships = collector.GetUsers();

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<UserMachineRelationship>));

            using (var output = File.Create("relationships-cmpivot.bin"))
            {
                rttModel.Serialize(output, relationships);
            }
            Debug.Print("Serialized relationships to relationships-cmpivot.bin");
        }

        // Helper to deserialize relationship objects
        public static List<UserMachineRelationship> DeserializeRelationshipsCMPivot()
        {

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<UserMachineRelationship>));
            List<UserMachineRelationship> relationships;

            using (var input = File.OpenRead("relationships-cmpivot.bin"))
            {
                relationships = (List<UserMachineRelationship>)rttModel.Deserialize(input, null, typeof(List<UserMachineRelationship>));

            }
            Debug.Print("Deserialized relationships from relationships.bin");

            return relationships;
        }

    }
}
