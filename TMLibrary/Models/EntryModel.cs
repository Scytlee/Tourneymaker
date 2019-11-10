using System.Collections.Generic;

namespace TMLibrary.Models
{
    public class EntryModel
    {
        public string EntryName { get; set; }
        public List<PersonModel> EntryMembers { get; set; } = new List<PersonModel>();
    }
}
