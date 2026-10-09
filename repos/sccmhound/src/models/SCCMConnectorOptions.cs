using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound.src.models
{
    public class SCCMConnectorOptions
    {
        public string username;
        public string password;
        public string domain;

        public SCCMConnectorOptions(string username, string password, string domain)
        {
            this.username = username;
            this.password = password;
            this.domain = domain;
        }
    }
}
