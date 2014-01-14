/*using MonoTouch.UIKit;
using XibFree;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class FooterCell : BaseCell<FooterItem>
    {
        public FooterCell(string identifier)
            : base(identifier)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            Layout.AddSubView(new NativeView
            {
                View = new UIImageView(Assets.Footer()) {ContentMode = UIViewContentMode.Center},
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = 33,
                    MarginTop = 10
                }
            });
            SelectionStyle = UITableViewCellSelectionStyle.None;
        }

        protected override void SetData(FooterItem item)
        {
        }
    }

    public class FooterItem : Item
    {
        public override BaseCell CreateCell()
        {
            return new FooterCell(GetIdentifier());
        }
    }
}*/