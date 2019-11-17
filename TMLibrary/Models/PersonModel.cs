namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one person.
    /// </summary>
    public class PersonModel : IModel
    {
        /// <summary>
        /// The unique identifier for the person.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The nickname of the person (eg. Minecraft name).
        /// </summary>
        public string Nickname { get; set; }

        /// <summary>
        /// The first name of the person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the person.
        /// </summary>
        public string LastName { get; set; }
        
        // TODO Replace Discord tag with Discord userID
        /// <summary>
        /// The Discord tag of the person.
        /// </summary>
        public string DiscordTag { get; set; }

        /// <summary>
        /// The primary email address of the person.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The display name for the person - full name, nickname, or both.
        /// </summary>
        public string DisplayName
        {
            get
            {
                string output;

                if (Nickname == "")
                {
                    output = $"{ FirstName } { LastName }";
                }
                else if (FirstName == "" && LastName == "")
                {
                    output = $"'{ Nickname }'";
                }
                else
                {
                    output = $"{ FirstName } { LastName } '{ Nickname }'";
                }

                return output;
            }
        }

    }
}
