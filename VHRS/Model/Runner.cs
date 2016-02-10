﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VHRS.Resources;

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

        /// <summary>
        /// The maximum number of characters that <see cref="Name"/> can have.
        /// </summary>
        private const Int32 _nameLength = 18;

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

        #region Events
        
        /// <summary>
        /// Fired if a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Returns an error message describing the validation error with the <see cref="Name"/>
        /// property, or null if <see cref="Name"/> is valid.
        /// </summary>
        /// <returns></returns>
        private String ValidateName()
        {
            if(Name == null)
            {
                return String.Empty;
            }
            else if (Name.Length > _nameLength)
            {
                return String.Format(Language.StringTooLong, _nameLength);
            }
            else if (new Regex(@"\d").IsMatch(Name))
            {
                return Language.StringContainsNumbers
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
