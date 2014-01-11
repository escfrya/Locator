using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Locator.Mobile.DAL;
using Locator.Mobile.DAL.Tables;

namespace Locator.TaskAgent
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        static ScheduledAgent()
        {
            // Subscribe to the managed exception handler
            Deployment.Current.Dispatcher.BeginInvoke(delegate
            {
                Application.Current.UnhandledException += UnhandledException;
            });
        }

        /// Code to execute on Unhandled Exceptions
        private static void UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }

        protected override void OnInvoke(ScheduledTask task)
        {
            //IActionRepository actionRepository = new ActionRepository();
            //List<LocatorAction> actions = actionRepository.GetExpiredActions();
            //if (actions.Any())
            //{
            //   var content = "Задачи " +string.Join(" ", actions.Select(x => x.Text)) + " просрочены";
            //    var toast = new ShellToast { Title = "Locator", Content = content };
            //    toast.Show(); 
            //}
            
            //NotifyComplete();
        }
    }
}