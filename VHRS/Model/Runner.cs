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
        
        public static readonly String NameProperty = "Name";
        /// <summary>
        /// The name of this runner.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public static readonly String OddsProperty = "Odds";
        /// <summary>
        /// The fractional odds price represented as a string fraction, e.g. 1/2.
        /// </summary>
        public String Odds
        {
            get { return _odds; }
            set
            {
                _odds = value;
                OnPropertyChanged(nameof(Odds));
            }
        }

        /// <summary>
        /// Returns the validation error for this <see cref="Runner"/>.
        /// </summary>
        public String Error
        {
            get
            {
                String nameError = ValidateName();
                String oddsError = ValidateOdds();

                if(String.IsNullOrEmpty(nameError))
                {
                    return oddsError;
                }
                else if (String.IsNullOrEmpty(oddsError))
                {
                    return nameError;
                }
                else
                {
                    return $"{nameError}; {oddsError}";
                }
            }
        }

        /// <summary>
        /// Returns the validation error for the property with the given <paramref name="columnName"/>.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public String this[String columnName]
        {
            get
            {
                if (columnName == null) return String.Empty;

                if(columnName.Equals(nameof(Name)))
                {
                    return ValidateName();
                }
                else if (columnName.Equals(nameof(Odds)))
                {
                    return ValidateOdds();
                }
                else
                {
                    return String.Empty;
                }
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new <see cref="Runner"/> from the given <paramref name="name"/>
        /// and <paramref name="odds"/>.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="odds"></param>
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
        /// Calculates the race margin for this <see cref="Runner"/>.
        /// </summary>
        /// <returns></returns>
        public Decimal GetMargin()
        {
            String numeratorPattern = "^([0-9]+)/";
            String denominatorPattern = "/([0-9]+)";

            if (!Regex.IsMatch(Odds, numeratorPattern) || !Regex.IsMatch(Odds, denominatorPattern)) return 0;

            Decimal numerator;
            Decimal denominator;
            try
            {
                Match numeratorMatch = Regex.Match(Odds, numeratorPattern);
                if (numeratorMatch.Groups.Count == 1) return 0;
                numerator = Convert.ToInt32(numeratorMatch.Groups[1].ToString());

                Match denominatorMatch = Regex.Match(Odds, denominatorPattern);
                if (denominatorMatch.Groups.Count == 1) return 0;
                denominator = Convert.ToInt32(denominatorMatch.Groups[1].ToString());
            }
            catch (InvalidCastException ex)
            {
                return 0;
            }
            catch (FormatException ex)
            {
                return 0;
            }

            return 100 / ((numerator / denominator) + 1);
        }

        /// <summary>
        /// Returns an error message describing the validation error with the <see cref="Name"/>
        /// property, or null if <see cref="Name"/> is valid.
        /// </summary>
        /// <returns></returns>
        private String ValidateName()
        {
            if(String.IsNullOrWhiteSpace(Name))
            {
                return Language.StringIsEmpty;
            }
            else if (Name.Length > _nameLength)
            {
                return String.Format(Language.StringTooLong, _nameLength);
            }
            else if (Regex.IsMatch(Name,@"\d"))
            {
                return Language.StringContainsNumbers;
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Returns an error message describing the validation error with the <see cref="Odds"/>
        /// property, or null if <see cref="Odds"/> is valid.
        /// </summary>
        /// <returns></returns>
        private String ValidateOdds()
        {
            if(String.IsNullOrWhiteSpace(Odds))
            {
                return Language.StringIsEmpty;
            }
            else if (!Regex.IsMatch(Odds,"^[0-9]+/[0-9]+$"))
            {
                return Language.InvalidOdds;
            }
            else
            {
                return String.Empty;
            }
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
