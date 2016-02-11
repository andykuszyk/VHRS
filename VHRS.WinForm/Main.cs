using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VHRS.ViewModel;

namespace VHRS.WinForm
{
    public partial class Main : Form
    {
        public MainViewModel ViewModel { get; }

        public Main(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            InitializeComponent();
        }
    }
}
