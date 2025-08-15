using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound.src.models
{
    public class ApplicationInstallation
    {
        public string applicationName { get; set; }
        public string systemResourceID { get; set; }

        public int applicationAddressSpace { get; set; }

        public ApplicationInstallation(string applicationName, string systemResourceID, int applicationAddressSpace)
        {
            this.applicationName = applicationName;
            this.systemResourceID = systemResourceID;
            this.applicationAddressSpace = applicationAddressSpace;
        }
    }
}
