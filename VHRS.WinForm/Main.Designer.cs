using VHRS.Model;
using VHRS.WinForm.Resources;

namespace VHRS.WinForm
{
    partial class Main
    {
        #region Fields
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.DataGridView runnerGrid;
        private System.Windows.Forms.GroupBox runnersGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn runnerGridOddsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn runnerGridNameCol;
        private System.Windows.Forms.Button addRunner; 
        private System.Windows.Forms.Button removeRunner;
        private System.Windows.Forms.Button runRace;
        #endregion

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
            this.runnerGrid = new System.Windows.Forms.DataGridView();
            this.runnersGroup = new System.Windows.Forms.GroupBox();
            this.runnerGridNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runnerGridOddsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addRunner = new System.Windows.Forms.Button();
            this.removeRunner = new System.Windows.Forms.Button();
            this.runRace = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.runnerGrid)).BeginInit();
            this.runnersGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // _runnerGrid
            // 
            this.runnerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.runnerGrid.AutoGenerateColumns = false;
            this.runnerGrid.DataSource = this.ViewModel.Runners;
            this.runnerGrid.Columns.Clear();
            this.runnerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.runnerGridNameCol,
            this.runnerGridOddsCol});
            this.runnerGrid.Location = new System.Drawing.Point(12, 19);
            this.runnerGrid.Name = "runnerGrid";
            this.runnerGrid.Size = new System.Drawing.Size(623, 525);
            this.runnerGrid.TabIndex = 0;
            this.runnerGrid.AllowUserToAddRows = true;
            this.runnerGrid.AllowUserToDeleteRows = true;
            // 
            // runnersGroup
            // 
            this.runnersGroup.Controls.Add(this.runnerGrid);
            this.runnersGroup.Location = new System.Drawing.Point(0, 0);
            this.runnersGroup.Name = "runnersGroup";
            this.runnersGroup.Size = new System.Drawing.Size(656, 550);
            this.runnersGroup.TabIndex = 1;
            this.runnersGroup.TabStop = false;
            this.runnersGroup.Text = Language.RunnersGroup_Text;
            // 
            // runnerGridNameCol
            // 
            this.runnerGridNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.runnerGridNameCol.HeaderText = global::VHRS.WinForm.Resources.Language.RunnerGrid_Name;
            this.runnerGridNameCol.Name = "runnerGridNameCol";
            this.runnerGridNameCol.DataPropertyName = Runner.NameProperty;
            // 
            // runnerGridOddsCol
            // 
            this.runnerGridOddsCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.runnerGridOddsCol.HeaderText = global::VHRS.WinForm.Resources.Language.RunnerGrid_Odds;
            this.runnerGridOddsCol.Name = "runnerGridOddsCol";
            this.runnerGridOddsCol.DataPropertyName = Runner.OddsProperty;
            // 
            // Add runner
            // 
            this.addRunner.Location = new System.Drawing.Point(662, 12);
            this.addRunner.Name = "addRunner";
            this.addRunner.Size = new System.Drawing.Size(75, 23);
            this.addRunner.TabIndex = 2;
            this.addRunner.Text = "Add Runner";
            this.addRunner.UseVisualStyleBackColor = true;
            // 
            // Remove runner
            // 
            this.removeRunner.Location = new System.Drawing.Point(662, 37);
            this.removeRunner.Name = "removeRunner";
            this.removeRunner.Size = new System.Drawing.Size(75, 23);
            this.removeRunner.TabIndex = 2;
            this.removeRunner.Text = "Remove Runner";
            this.removeRunner.UseVisualStyleBackColor = true;
            // 
            // Run race
            // 
            this.runRace.Location = new System.Drawing.Point(662, 60);
            this.runRace.Name = "runRace";
            this.runRace.Size = new System.Drawing.Size(75, 23);
            this.runRace.TabIndex = 2;
            this.runRace.Text = "Run Race";
            this.runRace.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.runnersGroup);
            this.Controls.AddRange(new[] { addRunner, removeRunner, runRace });
            this.Name = "Main";
            this.Text = Language.Main_Text;
            ((System.ComponentModel.ISupportInitialize)(this.runnerGrid)).EndInit();
            this.runnersGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
    }
}

