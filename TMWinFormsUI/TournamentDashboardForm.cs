using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMLibrary;
using TMLibrary.Models;

namespace TMWinFormsUI
{
    public partial class TournamentDashboardForm : Form
    {
        private List<TournamentModel> _tournaments = GlobalConfig.Connection.LoadTournamentModels();

        private List<TournamentPreviewModel> _readyToStartTournaments;
        private List<TournamentPreviewModel> _inProgressTournaments;
        private List<TournamentPreviewModel> _finishedTournaments;
        private List<TournamentPreviewModel> _tournamentsList = new List<TournamentPreviewModel>();

        public TournamentDashboardForm()
        {
            InitializeComponent();

            LoadLists();
        }

        // TODO Refactor this
        private void LoadLists()
        {
            _tournamentsList.Clear();

            if (showReadyToStartCheckBox.Checked)
            {
                if (_readyToStartTournaments == null)
                {
                    _readyToStartTournaments = GlobalConfig.Connection.LoadTournamentPreviews(TournamentStatus.ReadyToStart); 
                }
                _tournamentsList.AddRange(_readyToStartTournaments);
            }
            if (showInProgressCheckBox.Checked)
            {
                if (_inProgressTournaments == null)
                {
                    _inProgressTournaments = GlobalConfig.Connection.LoadTournamentPreviews(TournamentStatus.InProgress);
                }
                _tournamentsList.AddRange(_inProgressTournaments);
            }
            if (showFinishedCheckBox.Checked)
            {
                if (_finishedTournaments == null)
                {
                    _finishedTournaments = GlobalConfig.Connection.LoadTournamentPreviews(TournamentStatus.Finished);
                }
                _tournamentsList.AddRange(_finishedTournaments);
            }

            RefreshView();
        }

        private void RefreshView()
        {
            loadTournamentDropDown.DataSource = null;
            loadTournamentDropDown.DataSource = _tournamentsList;
            loadTournamentDropDown.DisplayMember = nameof(TournamentModel.TournamentName);
        }

        private void createNewTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm form = new CreateTournamentForm();
            form.Show();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            int tournamentId = ((TournamentPreviewModel) loadTournamentDropDown.SelectedItem).Id;
            TournamentModel selectedTournament = GlobalConfig.Connection.LoadTournamentModel(tournamentId);
            TournamentViewerForm form = new TournamentViewerForm(selectedTournament);
            form.Show();
        }

        private void showReadyToStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadLists();
        }

        private void showInProgressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadLists();
        }

        private void showFinishedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            LoadLists();
        }
    }
}
