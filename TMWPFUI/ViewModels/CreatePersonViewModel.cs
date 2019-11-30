using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TMLibrary;
using TMLibrary.Helpers;
using TMLibrary.Models;

namespace TMWPFUI.ViewModels
{
    public class CreatePersonViewModel : Screen
    {
        private string _nickname;
        private string _firstName;
        private string _lastName;
        private string _discordTag;
        private string _emailAddress;

        public string Nickname
        {
            get { return _nickname; }
            set
            {
                _nickname = value; 
                NotifyOfPropertyChange(() => Nickname);
                NotifyOfPropertyChange(() => CanCreatePerson);
            }
        }

        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => CanCreatePerson);
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => CanCreatePerson);
            }
        }

        public string DiscordTag
        {
            get { return _discordTag; }
            set
            {
                _discordTag = value;
                NotifyOfPropertyChange(() => DiscordTag);
                NotifyOfPropertyChange(() => CanCreatePerson);
            }
        }

        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                _emailAddress = value;
                NotifyOfPropertyChange(() => EmailAddress);
                NotifyOfPropertyChange(() => CanCreatePerson);
            }
        }

        public void CancelCreation()
        {
            EventAggregationProvider.TMEventAggregator.PublishOnUIThread(new PersonModel());
            this.TryClose();
        }

        public bool CanCreatePerson
        {
            get
            {
                bool output = false;

                if (ValidationHelper.ValidatePersonForm(Nickname, FirstName, LastName, DiscordTag, EmailAddress))
                {
                    output = true;
                }

                return output;
            }
        }

        public void CreatePerson()
        {
            // Create the PersonModel
            PersonModel person = new PersonModel
            {
                Nickname = Nickname,
                FirstName = FirstName,
                LastName = LastName,
                DiscordTag = DiscordTag,
                EmailAddress = EmailAddress
            };

            // Add the PersonModel to the database
            GlobalConfig.Connection.CreatePerson(person);

            // Pass the entry back to the parent and close the form
            EventAggregationProvider.TMEventAggregator.PublishOnUIThread(person);
            this.TryClose();
        }
    }
}
