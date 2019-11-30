using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TMLibrary;
using TMLibrary.Helpers;
using TMLibrary.Models;

namespace TMWPFUI.ViewModels
{
    public class CreateTournamentViewModel : Conductor<object>.Collection.AllActive, IHandle<EntryModel>, IHandle<PersonModel>
    {
        private string _tournamentName;
        private BindingList<EntryModel> _availableEntries;
        private EntryModel _selectedEntryToAdd;
        private BindingList<EntryModel> _selectedEntries;
        private EntryModel _selectedEntryToRemove;

        private Screen _activeCreateEntryView;
        private Screen _activeCreatePersonView;

        private bool _selectedEntriesIsVisible = true;
        private bool _createEntryIsVisible = false;
        private bool _createPersonIsVisible = false;

        public CreateTournamentViewModel()
        {
            InitializeLists();
            EventAggregationProvider.TMEventAggregator.Subscribe(this);
        }

        private void InitializeLists()
        {
            AvailableEntries = new BindingList<EntryModel>(GlobalConfig.Connection.LoadEntryModels());
            SelectedEntries = new BindingList<EntryModel>();
        }

        public string TournamentName
        {
            get { return _tournamentName; }
            set
            {
                _tournamentName = value;
                NotifyOfPropertyChange(() => TournamentName);
                NotifyOfPropertyChange(() => CanCreateTournament);
            }
        }

        public BindingList<EntryModel> AvailableEntries
        {
            get { return _availableEntries; }
            set
            {
                _availableEntries = value;
                NotifyOfPropertyChange(() => AvailableEntries);
            }
        }

        public EntryModel SelectedEntryToAdd
        {
            get { return _selectedEntryToAdd; }
            set
            {
                _selectedEntryToAdd = value;
                NotifyOfPropertyChange(() => SelectedEntryToAdd);
                NotifyOfPropertyChange(() => CanAddEntry);
            }
        }

        public BindingList<EntryModel> SelectedEntries
        {
            get { return _selectedEntries; }
            set
            {
                _selectedEntries = value;
                NotifyOfPropertyChange(() => SelectedEntries);
                NotifyOfPropertyChange(() => CanCreateTournament);
            }
        }

        public EntryModel SelectedEntryToRemove
        {
            get { return _selectedEntryToRemove; }
            set
            {
                _selectedEntryToRemove = value;
                NotifyOfPropertyChange(() => SelectedEntryToRemove);
                NotifyOfPropertyChange(() => CanRemoveEntry);
            }
        }

        public Screen ActiveCreateEntryView
        {
            get { return _activeCreateEntryView; }
            set
            {
                _activeCreateEntryView = value;
                NotifyOfPropertyChange(() => ActiveCreateEntryView);
            }
        }

        public Screen ActiveCreatePersonView
        {
            get { return _activeCreatePersonView; }
            set
            {
                _activeCreatePersonView = value;
                NotifyOfPropertyChange(() => ActiveCreatePersonView);
            }
        }

        public bool SelectedEntriesIsVisible
        {
            get { return _selectedEntriesIsVisible; }
            set
            {
                _selectedEntriesIsVisible = value; 
                NotifyOfPropertyChange(() => SelectedEntriesIsVisible);
            }
        }

        public bool CreateEntryIsVisible
        {
            get { return _createEntryIsVisible; }
            set
            {
                _createEntryIsVisible = value;
                NotifyOfPropertyChange(() => CreateEntryIsVisible);
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

        public bool CanAddEntry
        {
            get
            {
                bool output = false;

                if (SelectedEntryToAdd != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void AddEntry()
        {
            SelectedEntries.Add(SelectedEntryToAdd);
            AvailableEntries.Remove(SelectedEntryToAdd);
            NotifyOfPropertyChange(() => CanCreateTournament);
        }

        public void CreateEntry()
        {
            ActiveCreateEntryView = new CreateEntryViewModel();
            Items.Add(ActiveCreateEntryView);

            SelectedEntriesIsVisible = false;
            CreateEntryIsVisible = true;
        }

        public bool CanRemoveEntry
        {
            get
            {
                bool output = false;

                if (SelectedEntryToRemove != null)
                {
                    output = true;
                }

                return output;
            }
        }

        public void RemoveEntry()
        {
            AvailableEntries.Add(SelectedEntryToRemove);
            SelectedEntries.Remove(SelectedEntryToRemove);
            NotifyOfPropertyChange(() => CanCreateTournament);
        }

        public void CreatePerson()
        {
            ActiveCreatePersonView = new CreatePersonViewModel();
            Items.Add(ActiveCreatePersonView);

            CreatePersonIsVisible = true;
        }

        public bool CanCreateTournament
        {
            get
            {
                bool output = false;

                if (ValidationHelper.ValidateTournamentForm(TournamentName, SelectedEntries))
                {
                    output = true;
                }

                return output;
            }
        }

        public void CreateTournament()
        {
            // Create the TournamentModel
            TournamentModel tournament = new TournamentModel
            {
                TournamentName = TournamentName,
                TournamentEntries = SelectedEntries.ToList(),
                CurrentRound = 0
            };

            // Wire up matchups
            TournamentLogic.CreateRounds(tournament);

            // Create Tournament entry
            // Create all TournamentEntries
            GlobalConfig.Connection.CreateTournament(tournament);

            // Handle bye matchups
            TournamentLogic.HandleByeMatchups(tournament);

            EventAggregationProvider.TMEventAggregator.PublishOnUIThread(tournament);
            this.TryClose();
        }

        public void Handle(EntryModel entry)
        {
            // Check if model passed is valid
            if (entry.EntryMembers?.Count > 0)
            {
                SelectedEntries.Add(entry);
            }

            SelectedEntriesIsVisible = true;
            CreateEntryIsVisible = false;

            NotifyOfPropertyChange(() => CanCreateTournament);
        }

        public void Handle(PersonModel person)
        {
            CreatePersonIsVisible = false;
        }
    }
}
