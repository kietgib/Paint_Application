using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Midterm
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        private void Buttonbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = Buttonbox.SelectedItem.ToString();
        }
    }
}
