using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemoteTaskManager.presenters;
namespace RemoteTaskManager
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Client client = new Client();

            CreateForm myCreateForm =  new CreateForm();
            CreatePresenter createPresenter = new CreatePresenter(myCreateForm);

            MainForm mainForm = new MainForm();
            MainPresenter mainPresenter = new MainPresenter(mainForm, client);
            mainPresenter.createForm = myCreateForm;

            Application.Run(mainForm);
        }
    }
}
