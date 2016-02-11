using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VHRS.Model;
using VHRS.ViewModel;
using VHRS.WinForm.Resources;

namespace VHRS.WinForm
{
    public partial class Main : Form
    {
        #region Fields

        private DataGridView _runnerGrid;
        private Button _addRunner;
        private Button _removeRunner;
        private Button _runRace;

        #endregion

        #region Properties
        public MainViewModel ViewModel { get; }
        #endregion

        #region Constructor
        public Main(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
        #endregion

        #region Methods
        private void InitializeComponent()
        {
            SuspendLayout();

            // Create controls and add to this form.
            _runnerGrid = CreateRunnersGrid();
            _addRunner = CreateButton(662, 12, "addRunner", "Add Runner");
            _removeRunner = CreateButton(662, 37, "removeRunner", "Remove Runner");
            _runRace = CreateButton(662, 60, "runRace", "Run Race");
            Controls.AddRange(new Control[] { _runnerGrid, _addRunner, _removeRunner, _runRace });

            ClientSize = new System.Drawing.Size(784, 562);
            Name = "Main";
            Text = Language.Main_Text;

            ResumeLayout(false);
        }

        /// <summary>
        /// Creates a generic button with the given parameters.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private Button CreateButton(Int32 x, Int32 y, String name, String text)
        {
            Button button = new Button();
            button.Location = new System.Drawing.Point(x, y);
            button.Name = name;
            button.Size = new System.Drawing.Size(75, 23);
            button.TabIndex = 2;
            button.Text = text;
            button.UseVisualStyleBackColor = true;
            return button;
        }

        /// <summary>
        /// Creates a generic data grid column with the given parameters.
        /// </summary>
        /// <param name="header"></param>
        /// <param name="name"></param>
        /// <param name="propertyName">The name of the property on <see cref="Runner"/> that the column should display.</param>
        /// <returns></returns>
        private DataGridViewTextBoxColumn CreateDataGridColumn(String header, String name, String propertyName)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            col.HeaderText = header;
            col.Name = name;
            col.DataPropertyName = propertyName;
            return col;
        }

        /// <summary>
        /// Creates the main runners data grid.
        /// </summary>
        /// <returns></returns>
        private DataGridView CreateRunnersGrid()
        {
            DataGridView grid = new DataGridView();
            grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grid.AutoGenerateColumns = false;
            grid.DataSource = this.ViewModel.Runners;
            grid.Columns.Clear();
            grid.Columns.AddRange(new[]
            {
                CreateDataGridColumn(Language.RunnerGrid_Name, "runnerGridNameCol", Runner.NameProperty),
                CreateDataGridColumn(Language.RunnerGrid_Odds, "runnerGridOddsCol", Runner.OddsProperty),
            });
            grid.Location = new System.Drawing.Point(12, 19);
            grid.Name = "runnerGrid";
            grid.Size = new System.Drawing.Size(623, 525);
            grid.TabIndex = 0;
            grid.AllowUserToAddRows = true;
            grid.AllowUserToDeleteRows = true;
            return grid;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (ViewModel != null))
            {
                ViewModel.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
