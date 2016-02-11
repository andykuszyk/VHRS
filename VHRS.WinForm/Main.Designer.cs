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
            this._runnerGridNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._runnerGridOddsCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._runnerGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this._runnerGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._runnerGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._runnerGridNameCol,
            this._runnerGridOddsCol});
            this._runnerGrid.Location = new System.Drawing.Point(77, 62);
            this._runnerGrid.Name = "runnerGrid";
            this._runnerGrid.Size = new System.Drawing.Size(240, 150);
            this._runnerGrid.TabIndex = 0;
            // 
            // Name
            // 
            this._runnerGridNameCol.HeaderText = Language.RunnerGrid_Name;
            this._runnerGridNameCol.Name = "runnerGridNameCol";
            // 
            // Odds
            // 
            this._runnerGridOddsCol.HeaderText = Language.RunnerGrid_Odds;
            this._runnerGridOddsCol.Name = "runnerGridOddsCol";
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this._runnerGrid);
            this.Name = "Main";
            this.Text = "Virtual Horse Race Simulator";
            ((System.ComponentModel.ISupportInitialize)(this._runnerGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _runnerGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn _runnerGridNameCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn _runnerGridOddsCol;
    }
}

