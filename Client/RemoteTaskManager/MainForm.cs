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

namespace RemoteTaskManager
{
    public partial class MainForm : Form
    {
        private Client client;

        public MainForm()
        {
            InitializeComponent();

            listView1.View = View.Details;
            listView1.Columns.Add("File name", 100, HorizontalAlignment.Left);
        }

        public MainForm(Client client) : this()
        {
            this.client = client;

            client.connect();
            refreshListView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshListView();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            client.removeRemoteProcess(listView1.SelectedItems[0].Text);
            refreshListView();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CreateForm createForm = new CreateForm();

            createForm.ShowDialog();

            if (createForm.path != "")
            {
                client.createRemoteTask(createForm.path);
            }

            //client.createRemoteTask( @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" );
        }
        private void refreshListView()
        {
            listView1.Items.Clear();

            string[] processes = client.getAllRemoteTasks();

            foreach (string process in processes)
            {
                listView1.Items.Add(process);
            }
        }

    }
}
