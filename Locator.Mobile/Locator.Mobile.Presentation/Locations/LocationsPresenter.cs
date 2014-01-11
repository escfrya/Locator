﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locator.Mobile.BL.Client;
using Locator.Mobile.BL.ServiceClient;

namespace Locator.Mobile.Presentation
{
    public class LocationsPresenter : BasePresenter
    {
        private readonly ILocationsView view;

        public LocationsPresenter(ILocationsView view, IServiceCommandFactory factory, IDispatcher dispatcher, 
                                  INavigation navigation, ICacheHelper cacheHelper) 
            : base(view, factory, dispatcher, navigation, cacheHelper)
        {
            this.view = view;

            view.Refresh += ViewOnRefresh;
            view.OpenLocation += ViewOnOpenLocation;
        }

        private void ViewOnOpenLocation(long l)
        {
            
        }

        private void ViewOnRefresh()
        {
            ExecuteRequest(Factory.GetLocations(), view.Update);
        }
    }
}