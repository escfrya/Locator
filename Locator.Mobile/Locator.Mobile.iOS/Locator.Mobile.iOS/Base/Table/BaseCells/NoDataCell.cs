using MonoTouch.UIKit;
using XibFree;
using Locator.Mobile.iOS.Extentions;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class NoDataCell : BaseCell<NoDataItem>
    {
        private UILabel _label;

        public NoDataCell(string identifier)
            : base(identifier)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            Layout.AddSubView(new NativeView
            {
                View = _label = new UILabel {Lines = 0, BackgroundColor = UIColor.Clear},
                Init = c => c.As<UILabel>().Setup(LabelStyle),
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.WrapContent,
                    Gravity = Gravity.CenterHorizontal,
                    MarginTop = 20,
                    MarginBottom = 5
                }
            });
        }

        protected override void SetData(NoDataItem item)
        {
            _label.Text = item.Text;
        }

        private static void LabelStyle(UILabel obj)
        {
            obj.TextColor = UIColor.LightGray;
            obj.Font = UIFontExtentions.Light(12);
            obj.TextAlignment = UITextAlignment.Center;
        }
    }

    public class NoDataItem : Item
    {
        public NoDataItem(string text)
        {
            Text = text;
        }

        public string Text { get; set; }

        public override BaseCell CreateCell()
        {
            return new NoDataCell(GetIdentifier());
        }
    }
}