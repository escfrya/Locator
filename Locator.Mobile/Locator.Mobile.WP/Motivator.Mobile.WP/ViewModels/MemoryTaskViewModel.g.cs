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
	public partial class MemoryTaskViewModel : BaseViewModel<MemoryTaskPresenter>, IMemoryTaskView
	{
		private Motivator.Entity.Entities.MemTask task;
        public Motivator.Entity.Entities.MemTask Task 
        {
			get { return task; }
            set 
            { 
                task = value;
                OnPropertyChanged();
            }
        }

		private System.Collections.Generic.List<Motivator.Entity.Entities.Memory> memories;
        public System.Collections.Generic.List<Motivator.Entity.Entities.Memory> Memories 
        {
			get { return memories; }
            set 
            { 
                memories = value;
                OnPropertyChanged();
            }
        }

		private Motivator.Entity.Entities.Memory newmemory;
        public Motivator.Entity.Entities.Memory NewMemory 
        {
			get { return newmemory; }
            set 
            { 
                newmemory = value;
                OnPropertyChanged();
            }
        }

		public event System.EventHandler<System.EventArgs> GetNewTask;
		public ICommand GetNewTaskCommand { get; private set; }
		partial void GetNewTaskMethod(object o);
		public event System.EventHandler<Motivator.Mobile.Presentation.NewMemoryArgs> AddMemory;
		public ICommand AddMemoryCommand { get; private set; }
		partial void AddMemoryMethod(object o);
		public event System.EventHandler<System.EventArgs> Forgot;
		public ICommand ForgotCommand { get; private set; }
		partial void ForgotMethod(object o);
		public event System.EventHandler<Motivator.Mobile.Presentation.RefreshEventArgs> Refresh;
		public ICommand RefreshCommand { get; private set; }
		partial void RefreshMethod(object o);
		
		partial void Initialize();

		public MemoryTaskViewModel()
		{
			Initialize();
			GetNewTaskCommand = new Command(GetNewTaskMethod);
			AddMemoryCommand = new Command(AddMemoryMethod);
			ForgotCommand = new Command(ForgotMethod);
			RefreshCommand = new Command(RefreshMethod);

		}
	}
}