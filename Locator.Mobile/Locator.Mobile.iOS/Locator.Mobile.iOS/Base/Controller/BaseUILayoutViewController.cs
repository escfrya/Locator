using XibFree;
using Locator.Mobile.Presentation;

namespace VI.News.Client.iOS.Base.Controller
{
    public class BaseUILayoutViewController : BaseUIViewController
    {
        public BaseUILayoutViewController()
        {
            Layout = new LinearLayout(Orientation.Vertical)
            {
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.FillParent,
                }
            };
        }

        public LinearLayout Layout { get; protected set; }
        public UILayoutHost Host { get; protected set; }

        public override void LoadView()
        {
            base.LoadView();
            View = Host = new UILayoutHost(Layout);
        }
    }

    public abstract class BaseUILayoutViewController<TPresenter, TView> : BaseUILayoutViewController
        where TPresenter : class
		where TView : class, IBaseView
    {
        protected BaseUILayoutViewController()
        {
            Controller.ConstructPresenter<TPresenter, TView>(this);
        }
    }
}