namespace TMLibrary.Models
{
    /// <summary>
    /// Represents one person.
    /// </summary>
    public class PersonModel
    {
        /// <summary>
        /// The unique identifier for the person.
        /// </summary>
        public int Id { get; set; }

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
                if (Nickname == "")
                {
                    return $"{ FirstName } { LastName }";
                }
                else if (FirstName == "" && LastName == "")
                {
                    return Nickname;
                }
                else
                {
                    return $"{ FirstName } { LastName } ({ Nickname })";
                }
            }
        }

    }
}
