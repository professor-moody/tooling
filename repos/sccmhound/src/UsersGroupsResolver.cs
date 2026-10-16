using SCCMHound.src.models;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Group = SharpHoundCommonLib.OutputTypes.Group;

namespace SCCMHound.src
{
    public class UsersGroupsResolver
    {
        public UsersGroupsResolver(List<User> users, List<Group> groups)
        {
            Dictionary<string, Group> groupLookupByGroupName = HelperUtilities.createLookupTableGroups(groups);

            foreach (User user in users)
            {
                string[] sccmUserGroupNames = (string[])user.Properties["sccmUserGroupName"];

                foreach (string sccmUserGroupName in sccmUserGroupNames)
                {
                    string groupName = $"{sccmUserGroupName.Split('\\')[1].ToUpper()}@{user.Properties["domain"]}".ToLower();
                    if (groupLookupByGroupName.ContainsKey(groupName))
                    {
                        Group group = groupLookupByGroupName[groupName];
                        TypedPrincipal principal = new TypedPrincipal(user.ObjectIdentifier, SharpHoundCommonLib.Enums.Label.User);

                        List<TypedPrincipal> tempList = group.Members.ToList();
                        tempList.Add(principal);
                        group.Members = tempList.ToArray();
                    }
                }
                user.Properties.Remove("sccmUserGroupName");
            }

        }
    }
}
