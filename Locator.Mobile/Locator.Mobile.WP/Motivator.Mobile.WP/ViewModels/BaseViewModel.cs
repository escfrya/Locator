using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Motivator.Mobile.Presentation;
using Motivator.Mobile.WP.Annotations;
using TinyIoC;

namespace Motivator.Mobile.WP.ViewModels
{
    public class BaseViewModel<TPresenter> : INotifyPropertyChanged, IBaseView
        where TPresenter : class
    {
        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        private string busyText;
        public string BusyText
        {
            get { return busyText; }
            set { busyText = value; OnPropertyChanged(); }
        }

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

        public void MessageDialog(string message)
        {
            MessageBox.Show(message);
        }

        public bool OkCancelDialog(string message)
        {
            return MessageBox.Show(message, string.Empty, MessageBoxButton.OKCancel) == MessageBoxResult.OK;
        }

        
    }
}
