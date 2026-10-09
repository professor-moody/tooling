using SharpHoundCommonLib;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound.src.models
{
    public class ComputerLocalAdmins
    {
        public ComputerExt computer { get; set; }
        public List<User> users { get; set; } = new List<User>();
        public List<Group> groups { get; set; } = new List<Group>();

        public ComputerLocalAdmins(ComputerExt computer)
        {
            this.computer = computer;
        }

        public void AddAdminUser(User user)
        {
            this.users.Add(user);
        }

        public void AddAdminGroup(Group group)
        {
            this.groups.Add(group);
        }

        public void PopulateComputerExtWithLocalAdminData(string groupSID)
        {

            LocalGroupAPIResult admins = new LocalGroupAPIResult
            {
                ObjectIdentifier = groupSID,
                Name = $"ADMINISTRATORS@{this.computer.Properties["name"]}",

            };
            List<TypedPrincipal> tempList = new List<TypedPrincipal>();
            List<SHLocalAdmin> tempLocalAdmins = new List<SHLocalAdmin>();


            foreach (User user in users)
            {
                var res = new TypedPrincipal
                (
                    user.ObjectIdentifier,
                    SharpHoundCommonLib.Enums.Label.User
                   
                );

                tempList.Add(res);
                tempLocalAdmins.Add(new SHLocalAdmin(user.ObjectIdentifier, "User"));
            }

            foreach (Group group in groups)
            {
                var res = new TypedPrincipal
                (
                    group.ObjectIdentifier,
                    SharpHoundCommonLib.Enums.Label.Group

                );
                tempList.Add(res);
                tempLocalAdmins.Add(new SHLocalAdmin(group.ObjectIdentifier, "Group"));


            }
            admins.Results = tempList.ToArray();
            admins.Collected = true;
            this.computer.LocalAdmins = new SHLocalAdmins(tempLocalAdmins.ToArray());

        }

    }
}
