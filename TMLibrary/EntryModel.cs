using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMLibrary
{
    public class EntryModel
    {
        public string EntryName { get; set; }
        public List<PersonModel> EntryMembers { get; set; } = new List<PersonModel>();
    }
}
