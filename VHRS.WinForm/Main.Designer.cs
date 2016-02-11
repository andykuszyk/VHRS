using VHRS.WinForm.Resources;

namespace VHRS.WinForm
{
    partial class Main
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
            this._runnerGrid = new System.Windows.Forms.DataGridView();
            this.runnersGroup = new System.Windows.Forms.GroupBox();
            this.runnerGridNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runnerGridOddsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._runnerGrid)).BeginInit();
            this.runnersGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // _runnerGrid
            // 
            this._runnerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._runnerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.runnerGridNameCol,
            this.runnerGridOddsCol});
            this._runnerGrid.Location = new System.Drawing.Point(12, 19);
            this._runnerGrid.Name = "_runnerGrid";
            this._runnerGrid.Size = new System.Drawing.Size(623, 525);
            this._runnerGrid.TabIndex = 0;
            // 
            // runnersGroup
            // 
            this.runnersGroup.Controls.Add(this._runnerGrid);
            this.runnersGroup.Location = new System.Drawing.Point(0, 0);
            this.runnersGroup.Name = "runnersGroup";
            this.runnersGroup.Size = new System.Drawing.Size(656, 550);
            this.runnersGroup.TabIndex = 1;
            this.runnersGroup.TabStop = false;
            this.runnersGroup.Text = "Runners";
            // 
            // runnerGridNameCol
            // 
            this.runnerGridNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.runnerGridNameCol.HeaderText = global::VHRS.WinForm.Resources.Language.RunnerGrid_Name;
            this.runnerGridNameCol.Name = "runnerGridNameCol";
            // 
            // runnerGridOddsCol
            // 
            this.runnerGridOddsCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.runnerGridOddsCol.HeaderText = global::VHRS.WinForm.Resources.Language.RunnerGrid_Odds;
            this.runnerGridOddsCol.Name = "runnerGridOddsCol";
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.runnersGroup);
            this.Name = "Main";
            this.Text = "Virtual Horse Race Simulator";
            ((System.ComponentModel.ISupportInitialize)(this._runnerGrid)).EndInit();
            this.runnersGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _runnerGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _runnerGridNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn _runnerGridOddsCol;
        private System.Windows.Forms.GroupBox runnersGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn runnerGridOddsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn runnerGridNameCol;
    }
}

