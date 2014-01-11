//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Windows.Input;
using Motivator.Mobile.WP;
using Motivator.Mobile.Presentation;
using Motivator.Mobile.WP.ViewModels;

namespace Motivator.Mobile.WP.ViewModels
{
	public partial class MemoriesViewModel : BaseViewModel<MemoriesPresenter>, IMemoriesView
	{
		private System.Collections.Generic.List<Motivator.Entity.Entities.MemTask> memories;
        public System.Collections.Generic.List<Motivator.Entity.Entities.MemTask> Memories 
        {
			get { return memories; }
            set 
            { 
                memories = value;
                OnPropertyChanged();
            }
        }

		public event System.EventHandler<System.EventArgs> AddTask;
		public ICommand AddTaskCommand { get; private set; }
		partial void AddTaskMethod(object o);
		public event System.EventHandler<Motivator.Mobile.Presentation.OpenTaskArgs> OpenTask;
		public ICommand OpenTaskCommand { get; private set; }
		partial void OpenTaskMethod(object o);
		public event System.EventHandler<Motivator.Mobile.Presentation.RefreshEventArgs> Refresh;
		public ICommand RefreshCommand { get; private set; }
		partial void RefreshMethod(object o);
		
		partial void Initialize();

		public MemoriesViewModel()
		{
			Initialize();
			AddTaskCommand = new Command(AddTaskMethod);
			OpenTaskCommand = new Command(OpenTaskMethod);
			RefreshCommand = new Command(RefreshMethod);

		}
	}
}
