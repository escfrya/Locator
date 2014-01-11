using System;
using System.Windows;
using Motivator.Mobile.Presentation;

namespace Motivator.Mobile.WP
{
    public class Dispatcher : IDispatcher
    {
        public void Invoke(Action action)
        {
            Deployment.Current.Dispatcher.BeginInvoke(action.Invoke);
        }
    }
}
