using System.Drawing;
using MonoTouch.UIKit;
using XibFree;
using Locator.Mobile.Presentation;
using Locator.Mobile.iOS.Base;

namespace VI.News.Client.iOS.Base.Controller
{
    public class BaseUILayoutTableViewController : BaseUILayoutViewController
    {
        private readonly UITableViewStyle _style;

        protected BaseUILayoutTableViewController() : this(UITableViewStyle.Plain)
        {
        }

        protected BaseUILayoutTableViewController(UITableViewStyle withStyle)
        {
            _style = withStyle;
        }

        public UITableView TableView { get; private set; }

        public override void LoadView()
        {
            base.LoadView();
            Layout.AddSubView(new NativeView
            {
                View = TableView = new UITableView(RectangleF.Empty, _style),
                LayoutParameters = new LayoutParameters
                {
                    MarginTop = DeviceInfo.IOS7 ? 20 : 0,
                    Width = AutoSize.FillParent,
                    Height = AutoSize.FillParent
                }
            });
            TableView.BackgroundColor = UIColor.Clear;
        }
    }

    public abstract class BaseUILayoutTableViewController<TPresenter, TView> : BaseUILayoutTableViewController
        where TPresenter : class
		where TView : class, IBaseView
    {
        protected BaseUILayoutTableViewController()
        {
            Controller.ConstructPresenter<TPresenter, TView>(this);
        }

        protected BaseUILayoutTableViewController(UITableViewStyle withStyle) : base(withStyle)
        {
            Controller.ConstructPresenter<TPresenter, TView>(this);
        }
    }
}