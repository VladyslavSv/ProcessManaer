using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteTaskManager
{
    public partial class CreateForm : Form
    {
        public CreateForm()
        {
            InitializeComponent();
        }

        public string path
        {
            get
            {
                return processNameTextBox.Text;
            }
            set
            {
                processNameTextBox.Text = value;
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
