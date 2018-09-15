using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteTaskManager.interfaces;

namespace RemoteTaskManager.presenters
{
    public class CreatePresenter
    {
        public IViewCreateForm createFormView
        {
            get;set;
        }

        public CreatePresenter(IViewCreateForm createFormView)
        {
            this.createFormView = createFormView;

            createFormView.createForm += pressExecutor;
            createFormView.closeForm += pressExecutor;
        }

        public void pressExecutor(object sender, EventArgs e)
        {
            CreateForm createForm = createFormView as CreateForm;
            createForm.Close();
        }
    }
}
