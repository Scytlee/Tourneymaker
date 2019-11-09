using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary
{
    public class PersonModel
    {
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DiscordTag { get; set; }
        public string EmailAddress { get; set; }
    }
}
