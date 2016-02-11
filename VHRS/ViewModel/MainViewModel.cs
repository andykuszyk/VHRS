using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHRS.Model;

namespace VHRS.ViewModel
{
    public class MainViewModel
    {
        public BindingList<Runner> Runners { get; } = new BindingList<Runner>();

        public MainViewModel()
        {
            Runners.Add(new Runner("ANdy", "1/2"));
            Runners.Add(new Runner("ANdy123", "1a/2"));
        }
    }
}
