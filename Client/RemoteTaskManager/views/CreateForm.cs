using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteTaskManager.interfaces;
namespace RemoteTaskManager
{
    public partial class CreateForm : Form, IViewCreateForm
    {
        public event EventHandler createForm;
        public event EventHandler closeForm;

        public CreateForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
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
            createForm.Invoke(sender, e);
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            closeForm.Invoke(sender, e);
        }
    }
}
