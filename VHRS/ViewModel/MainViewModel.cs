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

        #endregion

        #region Properties

        /// <summary>
        /// The collection of <see cref="Runner"/>s that this instance represents.
        /// </summary>
        public BindingList<Runner> Runners { get; } = new BindingList<Runner>();

        /// <summary>
        /// The currently selected <see cref="Runner"/> in <see cref="Runners"/>.
        /// </summary>
        public Runner SelectedRunner { get; set; }


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
        }

        /// <summary>
        /// Removes <see cref="SelectedRunner"/> from <see cref="Runners"/>.
        /// </summary>
        public void RemoveRunner()
        {
            if (!_canRemoveRunner || SelectedRunner == null || !Runners.Contains(SelectedRunner)) return;
            Runners.Remove(SelectedRunner);
        } 

        #endregion
    }
}
