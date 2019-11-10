﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TMLibrary;

namespace TMWinFormsUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize the database connections
            TMLibrary.GlobalConfig.InitializeConnection(ConnectionType.SqlDatabase);

            Application.Run(new CreateEntryForm());
            
            //Application.Run(new TournamentDashboardForm());
        }
    }
}
