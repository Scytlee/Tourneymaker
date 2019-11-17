using System.Collections.Generic;

namespace TMLibrary.Models
{
    public class EntryModel : IModel
    {
        public int id { get; set; }
        public string EntryName { get; set; }
        public List<PersonModel> EntryMembers { get; set; } = new List<PersonModel>();

        public string DisplayName
        {
            get
            {
                // Empty name - 1 person in the entry
                if (string.IsNullOrWhiteSpace(EntryName))
                {
                    return EntryMembers[0].DisplayName;
                }
                // If not - the entry is a team
                else
                {
                    return EntryName;
                }
            }
        }
        public string DisplayNameWithTag
        {
            get
            {
                // Empty name - 1 person in the entry
                if (string.IsNullOrWhiteSpace(EntryName))
                {
                    return EntryMembers[0].DisplayName;
                }
                // If not - the entry is a team
                else
                {
                    return $"{ EntryName } [T]";
                }
            }
        }
    }
}
