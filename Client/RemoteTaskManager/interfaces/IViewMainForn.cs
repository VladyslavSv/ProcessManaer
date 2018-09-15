using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteTaskManager.interfaces
{
    public interface IViewMainForm
    {
        event EventHandler refreshProcessList;
        event EventHandler removeProcess;
        event EventHandler createProcess;

        string selectedItem
        {
            get;
        }
        void clearListView();
        void addItemsToListView(string[] processes);
    }
}
