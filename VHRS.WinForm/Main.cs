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
        private Label _raceMargin;

        #endregion

        #region Properties
        public MainViewModel ViewModel { get; }
        #endregion

        #region Constructor
        public Main(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            ViewModel.PropertiesChanged();
        }

        #endregion

        #region Methods
        private void ViewModel_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(ViewModel.CanAddRunner):
                    _addRunner.Enabled = ViewModel.CanAddRunner;
                    break;

                case nameof(ViewModel.CanRemoveRunner):
                    _removeRunner.Enabled = ViewModel.CanRemoveRunner;
                    break;

                case nameof(ViewModel.CanRunRace):
                    _runRace.Enabled = ViewModel.CanRunRace;
                    break;

                case nameof(ViewModel.RaceMargin):
                    _raceMargin.Text = String.Format(Language.RaceMargin, ViewModel.RaceMargin * 100);
                    break;
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();

            // Create controls and add to this form.
            _runnerGrid = CreateRunnersGrid();
            _runnerGrid.SelectionChanged += RunnerGrid_SelectionChanged;
            _addRunner = CreateButton(662, 12, "addRunner", Language.AddRunner);
            _addRunner.Click += AddRunner_Click;
            _removeRunner = CreateButton(662, 37, "removeRunner", Language.RemoveRunner);
            _removeRunner.Click += RemoveRunner_Click;
            _runRace = CreateButton(662, 60, "runRace", Language.RunRace);
            _runRace.Click += RunRace_Click;
            _raceMargin = CreateLabel(662, 83, "raceMargin", Language.RaceMargin);
            Controls.AddRange(new Control[] { _runnerGrid, _addRunner, _removeRunner, _runRace, _raceMargin });

            ClientSize = new System.Drawing.Size(784, 562);
            Name = "Main";
            Text = Language.Main_Text;

            ResumeLayout(false);
        }

        private void RunnerGrid_SelectionChanged(Object sender, EventArgs e)
        {
            if (_runnerGrid.CurrentRow == null) return;
            Runner runner = _runnerGrid.CurrentRow.DataBoundItem as Runner;
            ViewModel.SelectedRunner = runner;
        }

        private void RunRace_Click(Object sender, EventArgs e)
        {
            Runner winner = ViewModel.RunRace();
            if (winner == null) return;
            MessageBox.Show(String.Format(Language.RaceWinner,winner));
        }

        private void RemoveRunner_Click(Object sender, EventArgs e)
        {
            ViewModel.RemoveRunner();
        }

        private void AddRunner_Click(Object sender, EventArgs e)
        {
            ViewModel.AddRunner();
        }

        private ToolTip CreateToolTip(Button button)
        {
            ToolTip tooltip = new ToolTip();
            tooltip.SetToolTip(button, String.Empty);
            return tooltip;
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

        private Label CreateLabel(Int32 x, Int32 y, String name, String text)
        {
            Label label = new Label();
            label.Location = new Point(x, y);
            label.Name = name;
            label.Text = text;
            return label;
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
            grid.Location = new System.Drawing.Point(12, 12);
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
                ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
                ViewModel.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}
