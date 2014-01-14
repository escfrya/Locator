using MonoTouch.UIKit;
using XibFree;

namespace Locator.Mobile.iOS.Base.Table
{
    public abstract class BaseCell : UITableViewCell
    {
        protected BaseCell(string identifier) : base(UITableViewCellStyle.Default, identifier)
        {
        }

        public ViewGroup Layout { get; protected set; }
        public UILayoutHost Host { get; protected set; }

        /// <summary>
        ///     ios6 stub
        /// </summary>
        public override UIEdgeInsets SeparatorInset
        {
            get { return DeviceInfo.IOS6 ? UIEdgeInsets.Zero : base.SeparatorInset; }
            set
            {
                if (DeviceInfo.IOS6) return;
                base.SeparatorInset = value;
            }
        }

        public virtual void Initialize()
        {
            SeparatorInset = UIEdgeInsets.Zero;
            Layout = new LinearLayout(Orientation.Horizontal)
            {
                Padding = new UIEdgeInsets(5, 5, 5, 5),
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.WrapContent,
                }
            };

            ContentView.Add((Host = new UILayoutHost(Layout, ContentView.Bounds)));
            BackgroundColor = UIColor.Clear;
        }

        public abstract void SetCellData(object data);
        public abstract float CalculateHeight(UITableView tableView, object data);
    }


    public abstract class BaseCell<TItem> : BaseCell
        where TItem : Item
    {
        protected BaseCell(string identifier) : base(identifier)
        {
        }

        public TItem CurrentItem { get; private set; }

        protected virtual void SetData(TItem item)
        {
        }

        public override sealed void SetCellData(object data)
        {
            CurrentItem = (TItem) data;
            SetData(CurrentItem);
        }

        public override sealed float CalculateHeight(UITableView tableView, object data)
        {
            return MeasureHeight(tableView, (TItem) data);
        }

        protected virtual float MeasureHeight(UITableView tableView, TItem item)
        {
            SetData(item);
            Layout.Measure(tableView.Bounds.Width, float.MaxValue);
            return Layout.GetMeasuredSize().Height;
        }
    }
}