using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TMLibrary;
using TMLibrary.Models;
using TMLibrary.Helpers;

namespace TMWPFUI.ViewModels
{
    public class CreateEntryViewModel : Conductor<object>, IHandle<PersonModel>
    {
        private string _entryName;
        private BindingList<PersonModel> _availableEntryMembers;
        private PersonModel _selectedEntryMemberToAdd;
        private BindingList<PersonModel> _selectedEntryMembers;
        private PersonModel _selectedEntryMemberToRemove;

        private bool _selectedEntryMembersIsVisible = true;
        private bool _createPersonIsVisible = false;

        public CreateEntryViewModel()
        {
            InitializeLists();
            EventAggregationProvider.TMEventAggregator.Subscribe(this);
        }

        private void InitializeLists()
        {
            AvailableEntryMembers = new BindingList<PersonModel>(GlobalConfig.Connection.LoadPersonModels());
            SelectedEntryMembers = new BindingList<PersonModel>();
        }

        public string EntryName
        {
            get { return _entryName; }
            set
            {
                _entryName = value;
                NotifyOfPropertyChange(() => EntryName);
            }
        }

        public BindingList<PersonModel> AvailableEntryMembers
        {
            get { return _availableEntryMembers; }
            set
            {
                _availableEntryMembers = value;
                NotifyOfPropertyChange(() => AvailableEntryMembers);
            }
        }

        public PersonModel SelectedEntryMemberToAdd
        {
            get { return _selectedEntryMemberToAdd; }
            set
            {
                _selectedEntryMemberToAdd = value;
                NotifyOfPropertyChange(() => SelectedEntryMemberToAdd);
                NotifyOfPropertyChange(() => CanAddMember);
            }
        }

        public BindingList<PersonModel> SelectedEntryMembers
        {
            get { return _selectedEntryMembers; }
            set
            {
                _selectedEntryMembers = value;
                NotifyOfPropertyChange(() => SelectedEntryMembers);
                NotifyOfPropertyChange(() => CanCreateEntry);
            }
        }

        public PersonModel SelectedEntryMemberToRemove
        {
            get { return _selectedEntryMemberToRemove; }
            set
            {
                _selectedEntryMemberToRemove = value;
                NotifyOfPropertyChange(() => SelectedEntryMemberToRemove);
                NotifyOfPropertyChange(() => CanRemoveMember);
            }
        }

        public bool SelectedEntryMembersIsVisible
        {
            get { return _selectedEntryMembersIsVisible; }
            set
            {
                _selectedEntryMembersIsVisible = value; 
                NotifyOfPropertyChange(() => SelectedEntryMembersIsVisible);
            }
        }

        public bool CreatePersonIsVisible
        {
            get { return _createPersonIsVisible; }
            set
            {
                _createPersonIsVisible = value;
                NotifyOfPropertyChange(() => CreatePersonIsVisible);
            }
        }

        public bool CanAddMember
        {
            get
             {
                bool output = false;

                if (SelectedEntryMemberToAdd != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddMember()
        {
            SelectedEntryMembers.Add(SelectedEntryMemberToAdd);
            AvailableEntryMembers.Remove(SelectedEntryMemberToAdd);
            NotifyOfPropertyChange(() => CanCreateEntry);
        }

        public void CreateMember()
        {
            ActivateItem(new CreatePersonViewModel());
            SelectedEntryMembersIsVisible = false;
            CreatePersonIsVisible = true;
        }

        public bool CanRemoveMember
        {
            get
            {
                bool output = false;

                if (SelectedEntryMemberToRemove != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void RemoveMember()
        {
            AvailableEntryMembers.Add(SelectedEntryMemberToRemove);
            SelectedEntryMembers.Remove(SelectedEntryMemberToRemove);
            NotifyOfPropertyChange(() => CanCreateEntry);
        }

        public void CancelCreation()
        {
            EventAggregationProvider.TMEventAggregator.PublishOnUIThread(new EntryModel());
            this.TryClose();
        }

        public bool CanCreateEntry
        {
            get
            {
                bool output = false;

                if (ValidationHelper.ValidateEntryForm(EntryName, SelectedEntryMembers))
                {
                    output = true;
                }

                return output;
            }
        }

        public void CreateEntry()
        {
            // Create the EntryModel
            EntryModel entry = new EntryModel
            {
                EntryName = EntryName,
                EntryMembers = SelectedEntryMembers.ToList()
            };

            // Add the EntryModel to the database
            GlobalConfig.Connection.CreateEntry(entry);

            // Pass the entry back to the parent and close the form
            EventAggregationProvider.TMEventAggregator.PublishOnUIThread(entry);
            this.TryClose();
        }

        public void Handle(PersonModel person)
        {
            // Check if model passed is valid
            if (string.IsNullOrWhiteSpace(person.Nickname) == false && string.IsNullOrWhiteSpace(person.FirstName) == false)
            {
                SelectedEntryMembers.Add(person); 
            }

            SelectedEntryMembersIsVisible = true;
            CreatePersonIsVisible = false;

            NotifyOfPropertyChange(() => CanCreateEntry);
        }
    }
}
