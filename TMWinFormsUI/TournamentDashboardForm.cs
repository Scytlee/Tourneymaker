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
        private List<TournamentPreviewModel> _allTournaments;
        private List<TournamentPreviewModel> _readyToStartTournaments = new List<TournamentPreviewModel>();
        private List<TournamentPreviewModel> _inProgressTournaments = new List<TournamentPreviewModel>();
        private List<TournamentPreviewModel> _finishedTournaments = new List<TournamentPreviewModel>();
        private List<TournamentPreviewModel> _tournamentsDropDownList = new List<TournamentPreviewModel>();

        public TournamentDashboardForm()
        {
            InitializeComponent();

            InitializeLists();
        }

        private void InitializeLists()
        {
            _allTournaments = GlobalConfig.Connection.LoadTournamentPreviews();

            foreach (TournamentPreviewModel tournament in _allTournaments)
            {
                switch (tournament.Status)
                {
                    case TournamentStatus.ReadyToStart:
                        _readyToStartTournaments.Add(tournament);
                        break;
                    case TournamentStatus.InProgress:
                        _inProgressTournaments.Add(tournament);
                        break;
                    case TournamentStatus.Finished:
                        _finishedTournaments.Add(tournament);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _tournamentsDropDownList.AddRange(_inProgressTournaments);

            RefreshView();
        }

        private void UpdateDropDown(TournamentStatus status, bool inDropDown)
        {
            if (inDropDown)
            {
                switch (status)
                {
                    case TournamentStatus.ReadyToStart:
                        _tournamentsDropDownList.AddRange(_readyToStartTournaments);
                        break;
                    case TournamentStatus.InProgress:
                        _tournamentsDropDownList.AddRange(_inProgressTournaments);
                        break;
                    case TournamentStatus.Finished:
                        _tournamentsDropDownList.AddRange(_finishedTournaments);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(status), status, null);
                }
            }
            else
            {
                foreach (TournamentPreviewModel tournament in _allTournaments)
                {
                    if (tournament.Status == status)
                    {
                        _tournamentsDropDownList.Remove(tournament);
                    }
                }
            }

            RefreshView();
        }

        private void RefreshView()
        {
            loadTournamentDropDown.DataSource = null;
            loadTournamentDropDown.DataSource = _tournamentsDropDownList;
            loadTournamentDropDown.DisplayMember = nameof(TournamentModel.TournamentName);
        }

        private void createNewTournamentButton_Click(object sender, EventArgs e)
        {
            CreateTournamentForm form = new CreateTournamentForm();
            form.Show();
        }

        private void loadTournamentButton_Click(object sender, EventArgs e)
        {
            if (loadTournamentDropDown.SelectedItem != null)
            {
                int tournamentId = ((TournamentPreviewModel)loadTournamentDropDown.SelectedItem).id;
                TournamentModel selectedTournament = GlobalConfig.Connection.LoadTournamentModel(tournamentId);
                TournamentViewerForm form = new TournamentViewerForm(selectedTournament);
                form.Show(); 
            }
        }

        private void showReadyToStartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDropDown(TournamentStatus.ReadyToStart, showReadyToStartCheckBox.Checked);
        }

        private void showInProgressCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDropDown(TournamentStatus.InProgress, showInProgressCheckBox.Checked);
        }

        private void showFinishedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDropDown(TournamentStatus.Finished, showFinishedCheckBox.Checked);
        }
    }
}
