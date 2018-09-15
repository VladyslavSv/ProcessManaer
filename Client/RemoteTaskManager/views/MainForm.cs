using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using RemoteTaskManager.presenters;
using RemoteTaskManager.interfaces;

namespace RemoteTaskManager
{
    public partial class MainForm : Form, IViewMainForm
    {
        public event EventHandler refreshProcessList;
        public event EventHandler removeProcess;
        public event EventHandler createProcess;

        public string selectedItem
        {
            get
            {
                return listView1.SelectedItems[0].Text;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.Columns.Add("Process name", 435, HorizontalAlignment.Left);

            FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshProcessList.Invoke(sender, e);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            removeProcess.Invoke(sender, e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            createProcess.Invoke(sender, e);
            
        }

        public void clearListView()
        {
            listView1.Items.Clear();
        }
        public void addItemsToListView(string[] processes)
        {
            foreach (string process in processes)
            {
                listView1.Items.Add(process);
            }
        }


    }
}
