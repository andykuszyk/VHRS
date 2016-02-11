using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VHRS.Model;
using VHRS.Resources;

namespace VHRS.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Fields

        private Boolean _canAddRunner = true;
        private Boolean _canRemoveRunner = false;
        private Boolean _canRunRace = false;
        private Runner _selectedRunner = null;
        private Single _raceMargin = 0f;

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

        /// <summary>
        /// Returns true if the race can be run.
        /// </summary>
        public Boolean CanRunRace { get { return _canRunRace; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new view model with one <see cref="Runner"/> added to <see cref="Runners"/>.
        /// </summary>
        public MainViewModel()
        {
            AddRunner();
            OnPropertyChanged(nameof(CanAddRunner));
            OnPropertyChanged(nameof(CanRemoveRunner));
            OnPropertyChanged(nameof(CanRunRace));
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
        /// Called when a property changes on one of the <see cref="Runners"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Runner_PropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName.Equals(Runner.OddsProperty))
            {
                ReCalculateRaceMargin();
            }
        }

        /// <summary>
        /// Calculates the race margin for the <see cref="Runners"/>.
        /// </summary>
        private void ReCalculateRaceMargin()
        {
            Single raceMargin = 0f;
            String numeratorPattern = "^([0-9]+)/";
            String denominatorPattern = "/([0-9]+)";
            foreach (Runner runner in Runners)
            {
                if (!Regex.IsMatch(runner.Odds, numeratorPattern) || !Regex.IsMatch(runner.Odds, denominatorPattern)) continue;

                Int32 numerator;
                Int32 denominator;
                try
                {
                    Match numeratorMatch = Regex.Match(runner.Odds, numeratorPattern);
                    if (numeratorMatch.Groups.Count == 1) continue;
                    numerator = Convert.ToInt32(numeratorMatch.Groups[1]);

                    Match denominatorMatch = Regex.Match(runner.Odds, denominatorPattern);
                    if (denominatorMatch.Groups.Count == 1) continue;
                    denominator = Convert.ToInt32(denominatorMatch.Groups[1]);
                }
                catch(InvalidCastException)
                {
                    continue;
                }
                catch(FormatException)
                {
                    continue;
                }

                raceMargin += 100 / ((numerator / denominator) + 1);
            }

            _raceMargin = Convert.ToSingle(Math.Round(raceMargin, 2));
            EvaluateCanRunRace();
        }

        private void EvaluateCanRunRace()
        {
            _canRunRace = true;

            if(Runners.Count < 4 || Runners.Count > 16)
            {
                _canRunRace = false;
            }
            else if (_raceMargin < 1.1f || _raceMargin > 1.4f)
            {
                _canRunRace = false;
            }

            OnPropertyChanged(nameof(CanRunRace));
        }

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
            Runner runner = new Runner(String.Format(Language.DefaultRunnerName, Runners.Count + 1), Language.DefaultRunnerOdds);
            runner.PropertyChanged += Runner_PropertyChanged;
            Runners.Add(runner);

            ReCalculateRaceMargin();
            _canAddRunner = Runners.Count < _maxRunnerCount;
            OnPropertyChanged(nameof(CanAddRunner));
        }


        /// <summary>
        /// Removes <see cref="SelectedRunner"/> from <see cref="Runners"/>.
        /// </summary>
        public void RemoveRunner()
        {
            if (!_canRemoveRunner || SelectedRunner == null || !Runners.Contains(SelectedRunner)) return;
            Runners.Remove(SelectedRunner);
            SelectedRunner.PropertyChanged -= Runner_PropertyChanged;
            ReCalculateRaceMargin();
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

        public void Dispose()
        {
            foreach (Runner runner in Runners)
            {
                runner.PropertyChanged -= Runner_PropertyChanged;
            }
        }

        #endregion
    }
}
