using System;
using System.Collections.Generic;
using Locator.Mobile.BL.Client;
using Locator.ServiceContract;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation
{
    public class MemoriesPresenter : BasePresenter
    {
        private readonly IMemoriesView view;
        public MemoriesPresenter(IMemoriesView view, IServiceCommandFactory factory, IDispatcher dispatcher, INavigation navigation, ICacheHelper cacheHelper)
            : base(view, factory, dispatcher, navigation, cacheHelper)
        {
            this.view = view;

            view.AddTask += AddTask;
            view.Refresh += Refresh;
            view.OpenTask += OpenTask;
        }

        private void OpenTask(object sender, OpenTaskArgs e)
        {
			Navigation.Navigate(PageNames.MemoryTask, new Dictionary<string, int> {{"TaskId", e.TaskId}});
        }

        private void Refresh(object sender, EventArgs e)
        {
            ExecuteRequest(Factory.GetMemories(), model => {
				view.Memories = model.MemTasks;
				view.UpdateCallback(model.MemTasks);
			});
        }

        private void AddTask(object sender, EventArgs e)
        {
			Navigation.Navigate(PageNames.MemoryTask, new Dictionary<string, int>());
        }
    }
}
