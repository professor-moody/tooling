using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound
{
    // Not used within the project.
    public class SHReader
    {
        public List<User> users { get; set; }
        public SHReader(string filepath) {
            using (StreamReader sr = new StreamReader(filepath))
            {
                string json = sr.ReadToEnd();
                JObject o = JObject.Parse(json);
                users = o["data"].ToObject<List<User>>();
            }
        }

        public void addNetbios(Dictionary<string, string> fqdn2Netbios)
        {
            foreach (User user in users) {
                try
                {
                    string netbios = fqdn2Netbios[user.Properties["domain"].ToString()];
                    string netbiosComputerName = string.Format("{0}\\{1}", netbios, user.Properties["samaccountname"].ToString());
                    user.Properties.Add("sccmUniqueUserName", netbiosComputerName);
                }
                catch (KeyNotFoundException ex)
                {
                    Debug.Print("Could not resolve {0}", user.Properties["domain"]);
                }
            }
        }
    }
}
