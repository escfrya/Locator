using System;
using System.Windows;
using Locator.Mobile.Presentation;

namespace Locator.Mobile.WP
{
    public class Dispatcher : IDispatcher
    {
        public void Invoke(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action.Invoke);
        }
    }
}
