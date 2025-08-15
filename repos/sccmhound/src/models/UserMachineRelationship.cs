using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound.src.models
{
    public class UserMachineRelationship
    {
        public string resourceName { get; set; }
        public string uniqueUserName { get; set; }

        public UserMachineRelationship() { }

        public UserMachineRelationship(string resourceName, string uniqueUserName)
        {
            this.resourceName = resourceName;
            this.uniqueUserName = uniqueUserName;
        }
    }
}
