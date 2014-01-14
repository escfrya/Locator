using MonoTouch.UIKit;
using XibFree;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class LoadingCell : BaseCell<LoadingItem>
    {
        private readonly UIActivityIndicatorView _activity;
        private readonly bool _nomargin;

        public LoadingCell(string identifier, bool nomargin)
            : base(identifier)
        {
            _nomargin = nomargin;
            _activity = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
        }

        public override void Initialize()
        {
            base.Initialize();
            Layout.AddSubView(new NativeView
            {
                View = _activity,
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.WrapContent,
                    Gravity = Gravity.CenterHorizontal,
                    MarginTop = _nomargin ? 0 : 20
                }
            });
        }

        protected override void SetData(LoadingItem item)
        {
            _activity.StartAnimating();
        }
    }

    public class LoadingItem : Item
    {
        private readonly bool _nomargin;

        public LoadingItem(bool nomargin = false)
        {
            _nomargin = nomargin;
        }

        public override BaseCell CreateCell()
        {
            return new LoadingCell(GetIdentifier(), _nomargin);
        }
    }
}