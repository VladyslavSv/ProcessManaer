using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteTaskManager.interfaces;
using System.Windows.Forms;

namespace RemoteTaskManager.presenters
{
    public class MainPresenter
    {
        private IViewMainForm mainForm;
        public IViewCreateForm createForm;

        private Client client;

        public MainPresenter(MainForm mainForm, Client client)
        {

            this.mainForm = mainForm;
            this.client = client;

            this.mainForm.createProcess += onCreateProcess;
            this.mainForm.refreshProcessList += onRefreshProcesses;
            this.mainForm.removeProcess += onRemoveProcess;

            client.connect();
            refreshProcesses();
        }

        private void refreshProcesses()
        {
            mainForm.clearListView();
            mainForm.addItemsToListView(client.getAllRemoteTasks());
        }

        public void onRemoveProcess(object sender, EventArgs e)
        {
            client.removeRemoteProcess(mainForm.selectedItem);
            refreshProcesses();
        }

        public void onCreateProcess(object sender, EventArgs e)
        {
            CreateForm myCreateForm = createForm as CreateForm;

            myCreateForm.ShowDialog();

            if (createForm.path != "")
            {
                client.createRemoteTask(createForm.path);
            }
        }
        public void onRefreshProcesses(object sender, EventArgs e)
        {
            refreshProcesses();
        }
    }
}
