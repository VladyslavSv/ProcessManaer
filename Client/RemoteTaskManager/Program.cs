﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteTaskManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Client client = new Client();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(client));
        }
    }
}
