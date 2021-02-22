using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SGC_Basic_____project_
{
    public partial class F_carregar : Form
    {
        public F_carregar()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 4;

                
            }
            else
            {
                timer1.Enabled = false;
                Close();
                
            }
        }
    }
}
