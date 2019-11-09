namespace TMWinFormsUI
{
    partial class TournamentDashboardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerLabel = new System.Windows.Forms.Label();
            this.loadTournamentDropDown = new System.Windows.Forms.ComboBox();
            this.loadTournamentLabel = new System.Windows.Forms.Label();
            this.loadTournamentButton = new System.Windows.Forms.Button();
            this.createNewTournamentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Bahnschrift SemiBold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.White;
            this.headerLabel.Location = new System.Drawing.Point(9, 19);
            this.headerLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(449, 45);
            this.headerLabel.TabIndex = 5;
            this.headerLabel.Text = "Tourneymaker Dashboard";
            // 
            // loadTournamentDropDown
            // 
            this.loadTournamentDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.loadTournamentDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadTournamentDropDown.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadTournamentDropDown.ForeColor = System.Drawing.Color.White;
            this.loadTournamentDropDown.FormattingEnabled = true;
            this.loadTournamentDropDown.Location = new System.Drawing.Point(40, 149);
            this.loadTournamentDropDown.Name = "loadTournamentDropDown";
            this.loadTournamentDropDown.Size = new System.Drawing.Size(392, 41);
            this.loadTournamentDropDown.TabIndex = 7;
            // 
            // loadTournamentLabel
            // 
            this.loadTournamentLabel.AutoSize = true;
            this.loadTournamentLabel.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadTournamentLabel.Location = new System.Drawing.Point(34, 105);
            this.loadTournamentLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.loadTournamentLabel.Name = "loadTournamentLabel";
            this.loadTournamentLabel.Size = new System.Drawing.Size(222, 33);
            this.loadTournamentLabel.TabIndex = 6;
            this.loadTournamentLabel.Text = "Load tournament";
            // 
            // loadTournamentButton
            // 
            this.loadTournamentButton.BackColor = System.Drawing.Color.White;
            this.loadTournamentButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.loadTournamentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.loadTournamentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.loadTournamentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadTournamentButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadTournamentButton.ForeColor = System.Drawing.Color.Black;
            this.loadTournamentButton.Location = new System.Drawing.Point(351, 100);
            this.loadTournamentButton.Name = "loadTournamentButton";
            this.loadTournamentButton.Size = new System.Drawing.Size(81, 43);
            this.loadTournamentButton.TabIndex = 16;
            this.loadTournamentButton.Text = "Load";
            this.loadTournamentButton.UseVisualStyleBackColor = false;
            // 
            // createNewTournamentButton
            // 
            this.createNewTournamentButton.BackColor = System.Drawing.Color.White;
            this.createNewTournamentButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.createNewTournamentButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.createNewTournamentButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.createNewTournamentButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createNewTournamentButton.Font = new System.Drawing.Font("Bahnschrift Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.createNewTournamentButton.ForeColor = System.Drawing.Color.Black;
            this.createNewTournamentButton.Location = new System.Drawing.Point(56, 238);
            this.createNewTournamentButton.Name = "createNewTournamentButton";
            this.createNewTournamentButton.Size = new System.Drawing.Size(354, 65);
            this.createNewTournamentButton.TabIndex = 18;
            this.createNewTournamentButton.Text = "Create new tournament";
            this.createNewTournamentButton.UseVisualStyleBackColor = false;
            // 
            // TournamentDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(466, 354);
            this.Controls.Add(this.createNewTournamentButton);
            this.Controls.Add(this.loadTournamentButton);
            this.Controls.Add(this.loadTournamentDropDown);
            this.Controls.Add(this.loadTournamentLabel);
            this.Controls.Add(this.headerLabel);
            this.Font = new System.Drawing.Font("Bahnschrift SemiLight", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(8, 8, 8, 8);
            this.Name = "TournamentDashboardForm";
            this.Text = "Tourneymaker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label headerLabel;
        private System.Windows.Forms.ComboBox loadTournamentDropDown;
        private System.Windows.Forms.Label loadTournamentLabel;
        private System.Windows.Forms.Button loadTournamentButton;
        private System.Windows.Forms.Button createNewTournamentButton;
    }
}