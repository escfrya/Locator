using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Locator.Mobile.Presentation;
using Locator.Mobile.WP.Annotations;
using TinyIoC;

namespace Locator.Mobile.WP.ViewModels
{
    public class BaseViewModel<TPresenter> : INotifyPropertyChanged, IBaseView
        where TPresenter : class
    {
        protected TinyIoCContainer Container { get; private set; }

        public void ViewLoad()
        {
            Container = App.Container.GetChildContainer();
            Container.Resolve<TPresenter>(new NamedParameterOverloads(new Dictionary<string, object> {
                {"view", this}
            }));
        }

        public virtual void ParseParams(IDictionary<string, string> parameters)
        {
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion



        public bool Progress { set; private get; }
        public bool Transfer { set; private get; }
        public void ShowLockMessage(string message)
        {
            
        }

        public void HideLockMessage()
        {
            
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButton.OK);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK);
        }

        public void Invoke(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action);
        }

        public void Dispose()
        {
            
        }

        public bool WasRefreshed { set; private get; }
        public bool IsFirstLoad { set; private get; }
        public event Action Refresh;
    }
}
