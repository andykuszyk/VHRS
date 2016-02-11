using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHRS.Model;
using VHRS.Resources;

namespace VHRS.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields

        private Boolean _canAddRunner = true;
        private Boolean _canRemoveRunner = false;
        private Boolean _canRunRace = false;
        private Runner _selectedRunner = null;
        private String _addRunnerTooltip = String.Empty;
        private String _removeRunnerTooltip = String.Empty;
        private String _runRaceTooltip = String.Empty;

        /// <summary>
        /// The maximum number of runners that can be added to <see cref="Runners"/>.
        /// </summary>
        private Int32 _maxRunnerCount = 16;

        #endregion

        #region Properties

        /// <summary>
        /// The collection of <see cref="Runner"/>s that this instance represents.
        /// </summary>
        public BindingList<Runner> Runners { get; } = new BindingList<Runner>();

        /// <summary>
        /// The currently selected <see cref="Runner"/> in <see cref="Runners"/>.
        /// </summary>
        public Runner SelectedRunner
        {
            get { return _selectedRunner; }
            set
            {
                _selectedRunner = value;
                _canRemoveRunner = value != null;
                OnPropertyChanged(nameof(SelectedRunner));
                OnPropertyChanged(nameof(CanRemoveRunner));
            }
        }

        /// <summary>
        /// Returns true if the <see cref="RemoveRunner"/> method can be executed.
        /// </summary>
        public Boolean CanRemoveRunner { get { return _canRemoveRunner; } }

        /// <summary>
        /// Returns true if additional <see cref="Runner"/>s can be added to <see cref="Runners"/>.
        /// </summary>
        public Boolean CanAddRunner { get { return _canAddRunner; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new view model with one <see cref="Runner"/> added to <see cref="Runners"/>.
        /// </summary>
        public MainViewModel()
        {
            AddRunner();
        }

        #endregion

        #region Events

        /// <summary>
        /// Fired when a property value on this instance is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Calculates the winning <see cref="Runner"/>.
        /// </summary>
        /// <returns></returns>
        public Runner RunRace()
        {
            if (!_canRunRace) return null;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new <see cref="Runner"/> to <see cref="Runners"/>.
        /// </summary>
        public void AddRunner()
        {
            if (!_canAddRunner) return;
            Runners.Add(new Runner(String.Format(Language.DefaultRunnerName, Runners.Count + 1), Language.DefaultRunnerOdds));

            _canAddRunner = Runners.Count < _maxRunnerCount)
            OnPropertyChanged(nameof(CanAddRunner));
        }

        /// <summary>
        /// Removes <see cref="SelectedRunner"/> from <see cref="Runners"/>.
        /// </summary>
        public void RemoveRunner()
        {
            if (!_canRemoveRunner || SelectedRunner == null || !Runners.Contains(SelectedRunner)) return;
            Runners.Remove(SelectedRunner);
        }

        /// <summary>
        /// Fires the property changed event, if it has a subscriber.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
