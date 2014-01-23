using System;
using System.Windows;
using System.Windows.Navigation;
using Locator.Entity.Entities;
using Locator.Mobile.BL.Request;
using Locator.ServiceContract;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;
using Locator.Mobile.Client;
using Locator.Mobile.Presentation;
using Locator.Mobile.WP.ViewModels;
using Telerik.Windows.Controls;
using TinyIoC;

namespace Locator.Mobile.WP
{
    public partial class App : Application
    {
        private AppController appController;
        private IDispatcher dispatcher;

		/// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public static PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

				// Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        
    
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
		}

        private void AppInitialize()
        {
            var container = Bootstrapper.Initialize();
            RegisterComponents(container);
            Container = container;
            dispatcher = container.Resolve<IDispatcher>();
            appController = container.Resolve<AppController>();
            var isAuth = appController.AppStart();

            if (isAuth)
                OpenPushChannel();            
        }

        public static TinyIoCContainer Container { get; set; }

        private void RegisterComponents(TinyIoCContainer container)
        {
            container.Register<INavigation, Navigation>();
            container.Register<IDispatcher, Dispatcher>();
            container.Register<IFriendsView, FriendsViewModel>();
            container.Register<ILocationsView, LocationsViewModel>();
        }

        #region PushNotification
        private void OpenPushChannel()
        {
            const string channelName = "LocatorChannel";

            HttpNotificationChannel channel = HttpNotificationChannel.Find(channelName);

            if (channel == null)
            {
                channel = new HttpNotificationChannel(channelName);
                channel.Open();
                channel.BindToShellToast();

            }

            SendPushUri(channel.ChannelUri);

            channel.ChannelUriUpdated += ToastChannelOnChannelUriUpdated;
            channel.ErrorOccurred += ToastChannelOnErrorOccurred;
            channel.ShellToastNotificationReceived += ToastChannelOnShellToastNotificationReceived;
        }

        private void SendPushUri(Uri channelUri)
        {
            if (channelUri == null)
                return;

            appController.RegisterDevice(new DeviceDto()
                {
                    ClientVersion = (new Version(1,0,0,0)).ToString(),
                    OldDeviceAppId = null,
                    PlatformType = PlatformType.WindowsPhone,
                    DeviceAppId = channelUri.ToString()
                });
        }

        private void ToastChannelOnShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            var result = string.Format("{0} {1}", e.Collection["wp:Text1"], e.Collection["wp:Text2"]);
            
            dispatcher.Invoke(() => MessageBox.Show(result));
        }

        void ToastChannelOnErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            throw new Exception(e.Message);
        }

        void ToastChannelOnChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            SendPushUri(e.ChannelUri);
        }
        #endregion

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
 
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
			// Ensure that required application state is persisted here.
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new RadPhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;

            AppInitialize();
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}
