using MonoTouch.UIKit;
using XibFree;
using Locator.Mobile.iOS.Extentions;

namespace Locator.Mobile.iOS.Base.Table
{
    public abstract class BaseSectionView : UITableViewHeaderFooterView
    {
        protected BaseSectionView(string reuseIdentifier) : base(reuseIdentifier.ToNSString())
        {
        }

        public abstract float CalculateHeight(UITableView tableView, Section data);

        public virtual void InitView(Section section)
        {
        }
    }

    public abstract class BaseLayoutSectionView : BaseSectionView
    {
        protected BaseLayoutSectionView(string reuseIdentifier, Orientation orientation)
            : base(reuseIdentifier.ToNSString())
        {
            Layout = new LinearLayout(orientation)
            {
                LayoutParameters = new LayoutParameters {Width = AutoSize.FillParent, Height = AutoSize.WrapContent}
            };

            ContentView.Add((Host = new UILayoutHost(Layout, ContentView.Bounds)));
        }

        public ViewGroup Layout { get; protected set; }
        public UILayoutHost Host { get; protected set; }

        public override float CalculateHeight(UITableView tableView, Section data)
        {
            return MeasureHeight(tableView, data);
        }

        protected virtual float MeasureHeight(UITableView tableView, Section item)
        {
            InitView(item);
            Layout.Measure(tableView.Bounds.Width, float.MaxValue);
            return Layout.GetMeasuredSize().Height;
        }
    }
}