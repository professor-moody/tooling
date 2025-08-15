using SharpHoundCommonLib.OutputTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCMHound.src.models
{
    public class ComputerSessions
    {
        public ComputerExt computer {  get; set; }
        public List<User> users { get; set; } = new List<User>();

        public void AddSessionUser(User user)
        {
            this.users.Add(user);
        }

        public void PopulateComputerExtWithSessionData()
        {
            this.computer.Sessions = new SessionAPIResult();
            this.computer.Sessions.Collected = true;
            List<Session> sessions = new List<Session>();
            foreach (User user in users)
            {
                Session userSession = new Session();
                userSession.ComputerSID = computer.ObjectIdentifier;
                userSession.UserSID = user.ObjectIdentifier;
                sessions.Add(userSession);
            }

            this.computer.Sessions.Results = sessions.ToArray();

        }

        public ComputerSessions(ComputerExt computer)
        {
            this.computer = computer;
        }

        public void printComputerSessions()
        {
            string outUsers = "";
            foreach (User user in this.users)
            {
                outUsers = outUsers + user.Properties["name"] + ":";
            }

        }
    }
}
