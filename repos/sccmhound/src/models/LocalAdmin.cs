using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound.src.models
{
    [ProtoContract]

    public class LocalAdmin
    {
        [ProtoMember(1)]
        public string type;

        [ProtoMember(2)]
        public string name;

        [ProtoMember(3)]
        public string deviceName;

        public LocalAdmin(string type, string name, string deviceName)
        {
            this.type = type;
            this.name = name;
            this.deviceName = deviceName;
        }

        public LocalAdmin()
        {

        }
    }
}
