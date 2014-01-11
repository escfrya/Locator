using System;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.Request;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation
{
    public class MemoryTaskPresenter : BasePresenter
    {
        private readonly IMemoryTaskView view;
        //private readonly IActionRepository actionRepository;

        public MemoryTaskPresenter(IMemoryTaskView view, IServiceCommandFactory factory, IDispatcher dispatcher,
                                  INavigation navigation, ICacheHelper cacheHelper)
            : base(view, factory, dispatcher, navigation, cacheHelper)
        {
            this.view = view;
            //actionRepository = Container.Resolve<IActionRepository>();
            view.AddMemory += AddMemory;
            view.Forgot += Forgot;
            view.Refresh += Refresh;
            view.GetNewTask += GetNewTask;
        }

        private void Refresh(object sender, RefreshEventArgs e)
        {
            ExecuteRequest(Factory.GetMemoryTask(e.ID.ToString()), model =>
                {
                    view.Task = model.MemTask;
                    view.Memories = model.Memories;
				view.UpdateCallback(model);
                });
        }

        private void Forgot(object sender, EventArgs e)
        {
            view.NewMemory.NotRemember = true;
            ExecuteRequest(Factory.AddMemory(new AddMemoryRequest
            {
                memory = view.NewMemory
            }), memory => Navigation.GoBack());
        }

        private void AddMemory(object sender, NewMemoryArgs e)
        {
            view.NewMemory.MemTaskID = e.TaskId;
            ExecuteRequest(Factory.AddMemory(new AddMemoryRequest
                {
                    memory = view.NewMemory
                }), memory =>
                    {
                        //actionRepository.Save(motivAction.ToLocatorAction(), motivAction.ID.ToString());
                        Navigation.GoBack();
                    });
        }
        private void GetNewTask(object sender, EventArgs e)
        {
            ExecuteRequest(Factory.NewTask(new NewTaskRequest()), task => view.Task = task);
        }
    }
}