using SCCMHound.src;
using SCCMHound.src.models;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xunit;
using ProtoBuf;
using System.Diagnostics;
using ProtoBuf.Meta;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Text.RegularExpressions;
using Group = SharpHoundCommonLib.OutputTypes.Group;

namespace SCCMHound.tests
{
    public class SCCMCollectorTests
    {
        public static string testInstanceIP = TestConstants.testInstanceIP;
        public static string testInstanceSiteCode = TestConstants.testInstanceSiteCode;
        
        [Fact]
        public void QueryComputers_success()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            if (sccmCollector.QueryComputers().Count > 0)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void QueryUsers_success()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            if (sccmCollector.QueryUsers().Count > 0)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void QueryApplications_success()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            if (sccmCollector.QueryApplications().Count > 0)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void QueryUserMachineRelationships_success()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<UserMachineRelationship> relationships = sccmCollector.QueryUserMachineRelationships();
            if (relationships.Count > 0)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void QueryGroups_success()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<Group> groups = sccmCollector.QueryGroups();
            if (groups.Count > 0)
            {
                Assert.True(true);
            }
        }


        public class SerializableUser : User
        {
            public byte[] propertiesByteArray { get; set; }

            public SerializableUser() { }
            public SerializableUser(User user)
            {
                PropertyInfo[] properties = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(this, property.GetValue(user));
                    }
                }

                MemoryStream ms = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, user.Properties);
                this.propertiesByteArray = ms.ToArray();
            }

            public User toUser()
            {
                User user = new User();
                PropertyInfo[] properties = typeof(User).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(user, property.GetValue(this));
                    }
                }

                user.Properties = this.Properties;

                return user;
            }

            public void deserializeProperties()
            {
                MemoryStream ms = new MemoryStream(this.propertiesByteArray);
                var formatter = new BinaryFormatter();
                this.Properties = (Dictionary<string, Object>)formatter.Deserialize(ms);
            }
        }

        public class SerializableGroup : Group
        {
            public byte[] propertiesByteArray { get; set; }

            public SerializableGroup() { }
            public SerializableGroup(Group group)
            {
                PropertyInfo[] properties = typeof(Group).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(this, property.GetValue(group));
                    }
                }

                MemoryStream ms = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, group.Properties);
                this.propertiesByteArray = ms.ToArray();
            }

            public void deserializeProperties()
            {
                MemoryStream ms = new MemoryStream(this.propertiesByteArray);
                var formatter = new BinaryFormatter();
                this.Properties = (Dictionary<string, Object>)formatter.Deserialize(ms);
            }

            public Group toGroup()
            {
                Group group = new Group();
                PropertyInfo[] properties = typeof(Group).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(group, property.GetValue(this));
                    }

                }

                group.Properties = this.Properties;

                return group;
            }
        }

        // Run this to create a serialized groups.bin file for testing
        [Fact]
        public void SerializeGroups()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<Group> groups = sccmCollector.QueryGroups();

            List<SerializableGroup> sGroups = new List<SerializableGroup>();
            foreach (Group group in groups)
            {
                sGroups.Add(new SerializableGroup(group));
            }

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<SerializableGroup>));

            using (var output = File.Create("groups.bin"))
            {
                rttModel.Serialize(output, sGroups);
            }
            Debug.Print("Serialized groups to groups.bin");

            Assert.True(true);
        }

        // Helper to deserialize groups objects
        public static List<Group> DeserializeGroups()
        {

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<SerializableGroup>));
            List<SerializableGroup> groups;

            using (var input = File.OpenRead("groups.bin"))
            {
                groups = (List<SerializableGroup>)rttModel.Deserialize(input, null, typeof(List<SerializableGroup>));

                foreach (SerializableGroup group in groups)
                {
                    group.deserializeProperties();
                }

            }
            Debug.Print("Deserialized groups from groups.bin");

            List<Group> retGroups = new List<Group>();

            foreach (SerializableGroup sGroup in groups)
            {
                //Debug.Print("Name: {0}", user.Properties["name"]);
                /*
                foreach (KeyValuePair<string, object> prop in user.Properties)
                {
                    Debug.Print("{0}: {1}", prop.Key, prop.Value);
                }
                */

                retGroups.Add(sGroup.toGroup());
            }

            return retGroups;
        }

        // Run this to create a serialized users.bin file for testing
        [Fact]
        public void SerializeUsers()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<User> users = sccmCollector.QueryUsers();

            List<SerializableUser> sUsers = new List<SerializableUser>();
            foreach (User user in users) {
                sUsers.Add(new SerializableUser(user));
            }

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<SerializableUser>));

            using (var output = File.Create("users.bin"))
            {
                rttModel.Serialize(output, sUsers);
            }
            Debug.Print("Serialized users to users.bin");
            Assert.True(true);
        }

        // Helper to deserialize user objects
        public static List<User> DeserializeUsers()
        {
            
            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<SerializableUser>));
            List<SerializableUser> users;

            using (var input = File.OpenRead("users.bin"))
            {
                users = (List<SerializableUser>)rttModel.Deserialize(input, null, typeof(List<SerializableUser>));

                foreach (SerializableUser user in users)
                {
                    user.deserializeProperties();
                }

            }
            Debug.Print("Deserialized users from users.bin");

            List<User> retUsers = new List<User>();

            foreach (SerializableUser sUser in users)
            {
                //Debug.Print("Name: {0}", user.Properties["name"]);
                /*
                foreach (KeyValuePair<string, object> prop in user.Properties)
                {
                    Debug.Print("{0}: {1}", prop.Key, prop.Value);
                }
                */

                retUsers.Add(sUser.toUser());
            }

            return retUsers;
        }

        [Fact]
        public void DeserializeGroups_test()
        {
            List<Group> groups = DeserializeGroups();
            foreach (Group group in groups)
            {
                Debug.Print("{0}:{1}", group.ObjectIdentifier, group.Properties["name"]);
            }

            if (groups.Count > 0)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void DeserializeUsers_test()
        {
            List<User> users = DeserializeUsers();
            foreach (User user in users)
            {
                Debug.Print("{0}:{1}", user.ObjectIdentifier, user.Properties["name"]);
            }

            if (users.Count > 0)
            {
                Assert.True(true);
            }
        }

        public class SerializableComputerExt : ComputerExt
        {
            public byte[] propertiesByteArray { get; set; }

            public SerializableComputerExt() { }
            public SerializableComputerExt(ComputerExt computer)
            {
                PropertyInfo[] properties = typeof(ComputerExt).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(this, property.GetValue(computer));
                    }
                }

                MemoryStream ms = new MemoryStream();
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, computer.Properties);
                this.propertiesByteArray = ms.ToArray();
            }

            public void deserializeProperties()
            {
                MemoryStream ms = new MemoryStream(this.propertiesByteArray);
                var formatter = new BinaryFormatter();
                this.Properties = (Dictionary<string, Object>)formatter.Deserialize(ms);
            }

            public ComputerExt toComputerExt()
            {
                ComputerExt computer = new ComputerExt();
                PropertyInfo[] properties = typeof(ComputerExt).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo property in properties)
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(computer, property.GetValue(this));
                    }

                }

                computer.Properties = this.Properties;

                return computer;
            }
        }

        // Run this to create a serialized applications.bin file for testing
        [Fact]
        public void SerializeApplications()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<ApplicationInstallation> applicationInstallations = sccmCollector.QueryApplications();

            
            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<ApplicationInstallation>));

            using (var output = File.Create("applicationInstallations.bin"))
            {
                rttModel.Serialize(output, applicationInstallations);
            }
            Debug.Print("Serialized applications to applications.bin");
            Assert.True(true);
        }

        // Run this to create a serialized computers.bin file for testing
        [Fact]
        public void SerializeComputers()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<ComputerExt> computers = sccmCollector.QueryComputers();

            List<SerializableComputerExt> sComputers = new List<SerializableComputerExt>();
            foreach (ComputerExt computer in computers)
            {
                sComputers.Add(new SerializableComputerExt(computer));
            }

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<SerializableComputerExt>));

            using (var output = File.Create("computers.bin"))
            {
                rttModel.Serialize(output, sComputers);
            }
            Debug.Print("Serialized computers to computers.bin");
            Assert.True(true);
        }

        // Helper to deserialize computer objects
        public static List<ComputerExt> DeserializeComputers()
        {

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<SerializableComputerExt>));
            List<SerializableComputerExt> computers;

            using (var input = File.OpenRead("computers.bin"))
            {
                computers = (List<SerializableComputerExt>)rttModel.Deserialize(input, null, typeof(List<SerializableComputerExt>));

                foreach (SerializableComputerExt computer in computers)
                {
                    computer.deserializeProperties();
                }

            }
            Debug.Print("Deserialized computers from computers.bin");

            List<ComputerExt> retComputers = new List<ComputerExt>();

            foreach (SerializableComputerExt sComputer in computers)
            {
                //Debug.Print("Name: {0}", computer.Properties["name"]);
                /*
                foreach (KeyValuePair<string, object> prop in user.Properties)
                {
                    Debug.Print("{0}: {1}", prop.Key, prop.Value);
                }
                */
                retComputers.Add(sComputer.toComputerExt());
            }

            return retComputers;
        }

        [Fact]
        public void DeserializeComputers_test()
        {
            List<ComputerExt> computers = DeserializeComputers();
        }

        // Run this to create a serialized relationships.bin file for testing
        [Fact]
        public void SerializeRelationships()
        {
            SCCMConnector sccmConnector = SCCMConnector.CreateInstance(testInstanceIP, testInstanceSiteCode, null);
            SCCMCollector sccmCollector = new SCCMCollector(sccmConnector);
            List<UserMachineRelationship> relationships = sccmCollector.QueryUserMachineRelationships();

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<UserMachineRelationship>));

            using (var output = File.Create("relationships.bin"))
            {
                rttModel.Serialize(output, relationships);
            }
            Debug.Print("Serialized relationships to relationships.bin");
        }

        // Helper to deserialize relationship objects
        public static List<UserMachineRelationship> DeserializeRelationships()
        {

            var rttModel = RuntimeTypeModel.Default;
            HelperUtilities.ConfigureRttModel(rttModel, typeof(List<UserMachineRelationship>));
            List<UserMachineRelationship> relationships;

            using (var input = File.OpenRead("relationships.bin"))
            {
                relationships = (List<UserMachineRelationship>)rttModel.Deserialize(input, null, typeof(List<UserMachineRelationship>));

            }
            Debug.Print("Deserialized relationships from relationships.bin");


            foreach (UserMachineRelationship relationship in relationships)
            {
                //Debug.Print("{0}: {1}", relationship.resourceName, relationship.uniqueUserName);
                /*
                foreach (KeyValuePair<string, object> prop in user.Properties)
                {
                    Debug.Print("{0}: {1}", prop.Key, prop.Value);
                }
                */
            }

            return relationships;
        }
        [Fact]
        public void DeserializeRelationships_test()
        {
            List<UserMachineRelationship> relationships = DeserializeRelationships();

            foreach (UserMachineRelationship relationship in relationships)
            {
                Debug.Print($"{relationship.uniqueUserName}:{relationship.resourceName}");
            }

            if (relationships.Count > 0)
            {
                Assert.True(true);
            }
        }


    }
}
