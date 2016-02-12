using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private Decimal _raceMargin = 0;

        /// <summary>
        /// The minimum margin, below which races cannot be run.
        /// </summary>
        private const Decimal _minRaceMargin = 1.1M;

        /// <summary>
        /// The maximum margin, above which races cannot be run.
        /// </summary>
        private const Decimal _maxRaceMargin = 1.4M;

        /// <summary>
        /// The margin by which chances can vary in <see cref="RunRace"/>.
        /// </summary>
        private const Decimal _randomMargin = 0.02M;

        /// <summary>
        /// Used for calculating random variations in <see cref="RunRace"/>.
        /// </summary>
        private Random _random = new Random();

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

        /// <summary>
        /// The margin of the race if it was run now.
        /// </summary>
        public Decimal RaceMargin { get { return _raceMargin; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new view model with one <see cref="Runner"/> added to <see cref="Runners"/>.
        /// </summary>
        public MainViewModel()
        {
            AddRunner();
            PropertiesChanged();
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
        /// Fires the property changed notifications on the properties.
        /// </summary>
        public void PropertiesChanged()
        {
            OnPropertyChanged(nameof(CanAddRunner));
            OnPropertyChanged(nameof(CanRemoveRunner));
            OnPropertyChanged(nameof(CanRunRace));
        }

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
            _raceMargin = Math.Round(Runners.Sum(r => r.GetMargin()), 2);
            OnPropertyChanged(nameof(RaceMargin));
            EvaluateCanRunRace();
        }

        private void EvaluateCanRunRace()
        {
            _canRunRace = true;

            if(Runners.Count < 4 || Runners.Count > 16)
            {
                _canRunRace = false;
            }
            else if (_raceMargin < _minRaceMargin || _raceMargin > _maxRaceMargin)
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
            if (!_canRunRace || _raceMargin == 0) return null;

            // Calculate the chances of each of the runners winning, based on their margin.
            var runnersAndChances = Runners.Select(r => new { Runner = r, Chances = r.GetMargin() / _raceMargin });

            // Now, randomize the runners' chances using a randomization margin.
            var runnersAndRanomizedChances = runnersAndChances.Select(r => new { Runner = r.Runner, Chances = r.Chances + (Convert.ToDecimal(_random.NextDouble() * 2 - 1) * _randomMargin) });

            // Re-normalize the chances so that they stack up to 100%.
            Decimal totalRandomizedChances = runnersAndRanomizedChances.Sum(r => r.Chances);
            var runnersAndNormalizedRandomizedChances = runnersAndRanomizedChances.Select(r => new { Runner = r.Runner, Chances = r.Chances / totalRandomizedChances });

            // Calculate a random outcome for the race.
            Decimal raceOutcome = Convert.ToDecimal(_random.NextDouble());

            // Now, iterate over the runners to see if their chances of winning covers the random race outcome.
            Decimal cumulativeChances = 0M;
            foreach(var runnerChances in runnersAndNormalizedRandomizedChances.OrderBy(r => r.Chances))
            {
                Decimal runnerMinChance = cumulativeChances;
                Decimal runnerMaxChance = cumulativeChances + runnerChances.Chances;


                if (raceOutcome > runnerMinChance && raceOutcome <= runnerMaxChance)
                {
                    // If the runners chances covers the outcome, we have a winner.
                    // Note that this case covers 1.
                    return runnerChances.Runner;
                }
                else if(cumulativeChances == 0 && raceOutcome == 0)
                {
                    // If the race outcome was zero and this is the first runner, then this one wins.
                    return runnerChances.Runner;
                }

                // If we didn't find a winner, increase the cumulative chances and continue.
                cumulativeChances += runnerChances.Chances;
            }

            // A winner should always be found, so this point should never be reached.
            Debug.Fail($"A winner should always be found by {nameof(RunRace)}");
            return null;
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
