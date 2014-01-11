using System;
using System.Collections.Generic;
using Locator.Entity.Entities;
using Locator.ServiceContract.Models;

namespace Locator.Mobile.Presentation
{
    public class NewMemoryArgs : EventArgs
    {
        public int TaskId { get; set; }
    }
    public interface IMemoryTaskView : IBaseView
    {
        MemTask Task { get; set; }
        List<Memory> Memories { get; set; }
        Memory NewMemory { get; set; }
        event EventHandler<EventArgs> GetNewTask;
        event EventHandler<NewMemoryArgs> AddMemory;
        event EventHandler<EventArgs> Forgot;
        event EventHandler<RefreshEventArgs> Refresh;
		void UpdateCallback (MemoryModel model);
    }
}
