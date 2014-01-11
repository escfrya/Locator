using System;
using Microsoft.Phone.Scheduler;

namespace Locator.Mobile.WP.Helpers
{
    public static class TaskHelper
    {
        public static bool AgentsAreEnabled = true;

        public static void RegisterPeriodicTask(string taskName, string taskDesc)
        {
            StopAgentIfStarted(taskName);
            var task = new PeriodicTask(taskName) {Description = taskDesc};
            try
            {
                ScheduledActionService.Add(task);
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    AgentsAreEnabled = false;
                }
                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }

#if DEBUG
            if (AgentsAreEnabled)
            {
                ScheduledActionService.LaunchForTest(taskName, new TimeSpan(0,0,1));
            }
#endif
        }

        public static void StopAgent(string taskName)
        {
            StopAgentIfStarted(taskName);
        }

        private static void StopAgentIfStarted(string taskName)
        {
            if(ScheduledActionService.Find(taskName) != null)
                ScheduledActionService.Remove(taskName);
        }
    }
}
