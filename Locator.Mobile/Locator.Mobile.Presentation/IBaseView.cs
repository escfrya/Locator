using System;

namespace Locator.Mobile.Presentation
{
    public interface IViewMessage
    {
        void MessageDialog(string message);
        bool OkCancelDialog(string message);
    }

    public interface IBaseView : IViewMessage
    {
        bool IsBusy { get; set; }
        string BusyText { get; set; }
    }

    public class RefreshEventArgs : EventArgs
    {
        public int ID { get; set; }
    }
}
