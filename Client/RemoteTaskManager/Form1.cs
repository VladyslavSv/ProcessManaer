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
    public partial class Form1 : Form
    {
        Client client;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Client client)
        {
            InitializeComponent();

            client.connect();
            string[] processes = client.getAllRemoteTasks();

            foreach (string process in processes)
            {
                listView1.Items.Add(process);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            client = new Client();
            client.connect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.createRemoteTask( @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe" );
        }
    }
}
