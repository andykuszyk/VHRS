using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VHRS.WinForm.Resources;

namespace VHRS.WinForm
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.Text = Language.Main_Text;
            this.Width = 800;
            this.Height = 600;

        }
    }
}
