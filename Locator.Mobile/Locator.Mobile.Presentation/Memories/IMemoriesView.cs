using System;
using System.Collections.Generic;
using Locator.Entity.Entities;

namespace Locator.Mobile.Presentation
{
    public class OpenTaskArgs : EventArgs
    {
        public int TaskId { get; set; }
    }

    public interface IMemoriesView : IBaseView
    {
        List<MemTask> Memories { get; set; }
        event EventHandler<EventArgs> AddTask;
        event EventHandler<OpenTaskArgs> OpenTask;
        event EventHandler<RefreshEventArgs> Refresh;
		void UpdateCallback (List<MemTask> memories);
    }
}
