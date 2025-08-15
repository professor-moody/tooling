using System;
using System.Management;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using SCCMHound.src.models;

namespace SCCMHound
{

    public class SCCMConnector
    {
        public ManagementScope scope { get; set; }

        private SCCMConnector(ManagementScope scope)
        {
            this.scope = scope;
        }

        public static SCCMConnector CreateInstance(string mgmtServer, string siteCode, SCCMConnectorOptions sccmConnectorOptions)
        {
            try
            {
                string mgmtPath = $"\\\\{mgmtServer}\\root\\sms\\site_{siteCode}";
                Debug.Print(mgmtPath);

                ConnectionOptions options = new ConnectionOptions
                {
                    Timeout = TimeSpan.FromSeconds(180)
                };

                if (sccmConnectorOptions is not null)
                {
                    options.Username = $"{sccmConnectorOptions.domain}\\{sccmConnectorOptions.username}";
                    options.Password = sccmConnectorOptions.password;
                }

                ManagementScope scope = new ManagementScope(mgmtPath, options);
                scope.Connect();
                return new SCCMConnector(scope);
            }
            catch (Exception ex)
            {
                throw ex; // handle by invoker
            }
        }
    }
}
