using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHRS.Model
{
    /// <summary>
    /// Represents a runner in a race, encapsulating its <see cref="Name"/> and <see cref="Odds"/>.
    /// </summary>
    public class Runner : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// The name of this runner.
        /// </summary>
        private String _name;

        /// <summary>
        /// The fractional odds price represented as a string fraction, e.g. 1/2.
        /// </summary>
        private String _odds;

        #endregion

        #region Properties
        
        /// <summary>
        /// The name of this runner.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        } 

        /// <summary>
        /// The fractional odds price represented as a string fraction, e.g. 1/2.
        /// </summary>
        public String Odds
        {
            get { return _odds; }
            set
            {
                _odds = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Odds)));
            }
        }

        public String Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public String this[String columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region Constructors


        public Runner(String name, String odds)
        {
            _name = name;
            _odds = odds;
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
