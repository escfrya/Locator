using System;
using System.Collections.Generic;
using Motivator.Mobile.Presentation;

namespace Motivator.Mobile.WP
{
    public class Navigation : INavigation
    {
        public void Navigate(string pageName, Dictionary<string, int> parameters)
        {
            string pageUrl = string.Format("/Pages/{0}.xaml", pageName);
            if (parameters != null)
            {
                var prefix = "?";
                foreach (var parameter in parameters)
                {
                    pageUrl += string.Format("{0}{1}={2}", prefix, parameter.Key, parameter.Value);
                    prefix = "&";
                }
            }
            App.RootFrame.Navigate(new Uri(pageUrl, UriKind.Relative));
        }

        public void GoBack()
        {
            if (App.RootFrame.CanGoBack)
                App.RootFrame.GoBack();
        }
    }
}
