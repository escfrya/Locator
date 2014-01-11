using System;
using System.Collections.Generic;
using System.ComponentModel;
using Motivator.Entity.Entities;
using Motivator.Mobile.Presentation;
using Motivator.ServiceContract.Models;

namespace Motivator.Mobile.WP.ViewModels
{
    partial class MemoriesViewModel
    {
        partial void Initialize()
        {
            this.PropertyChanged += OnPropertyChanged;
        }

        private MemTask memoryTask;
        public MemTask MemoryTask
        {
            get { return memoryTask; }
            set { memoryTask = value; OnPropertyChanged(); }
        }

        private async void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Action")
            {

            }
        }
        partial void RefreshMethod(object o)
        {
            Refresh(this, new RefreshEventArgs());
        }
        partial void AddTaskMethod(object o)
        {
            AddTask(this, EventArgs.Empty);
        }

        partial void OpenTaskMethod(object o)
        {
            OpenTask(this, new OpenTaskArgs{TaskId = MemoryTask.ID});
        }

        public void UpdateCallback(List<MemTask> memories)
        {
            
        }
    }

    partial class MemoryTaskViewModel
    {

        public override void ParseParams(IDictionary<string, string> parameters)
        {
            if (!parameters.ContainsKey("TaskId"))
            {
                GetNewTask(this, EventArgs.Empty);
            }
            else
            {
                var taskId = int.Parse(parameters["TaskId"]);
                Refresh(this, new RefreshEventArgs { ID = taskId });
            }
        }

        partial void Initialize()
        {
            NewMemory = new Memory { MemTask = Task };
        }

        partial void ForgotMethod(object o)
        {
            Forgot(this, EventArgs.Empty);
        }

        partial void AddMemoryMethod(object o)
        {
            AddMemory(this, new NewMemoryArgs { TaskId = Task.ID });
        }

        partial void RefreshMethod(object o)
        {
            Refresh(this, new RefreshEventArgs { ID = Task.ID });
        }
        partial void GetNewTaskMethod(object o)
        {
            throw new NotImplementedException();
        }

        public void UpdateCallback(MemoryModel model)
        {
            
        }
    }
}
