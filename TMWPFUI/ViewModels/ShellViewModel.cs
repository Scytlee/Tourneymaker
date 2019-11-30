using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using TMLibrary;
using TMLibrary.Models;

namespace TMWPFUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private BindingList<TournamentPreviewModel> _existingTournaments;
        private TournamentModel _loadedTournament;

        public ShellViewModel()
        {
            // Initialize the database connections
            GlobalConfig.InitializeConnection(ConnectionType.TextFile);

            InitializeLists();
        }

        public void CreateTournament()
        {

        }

        private void InitializeLists()
        {
            _existingTournaments =
                new BindingList<TournamentPreviewModel>(GlobalConfig.Connection.LoadTournamentPreviews());
        }

        public BindingList<TournamentPreviewModel> ExistingTournaments
        {
            get { return _existingTournaments; }
            set
            {
                _existingTournaments = value; 
                NotifyOfPropertyChange(() => ExistingTournaments);
            }
        }

        private TournamentPreviewModel _selectedTournament;

        public TournamentPreviewModel SelectedTournament
        {
            get { return _selectedTournament; }
            set
            {
                if (value != null)
                {
                    _selectedTournament = value;
                    _loadedTournament = GlobalConfig.Connection.LoadTournamentModel(value.id);
                    NotifyOfPropertyChange(() => SelectedTournament);
                }
            }
        }
    }
}
