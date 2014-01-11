using System.Collections.Generic;

namespace Locator.Mobile.Presentation
{
    public interface INavigation
    {
        //void Navigate(string pageName, string param = null);
		void Navigate (string pageName, Dictionary<string, int> parameters);
        void GoBack();
    }
}
