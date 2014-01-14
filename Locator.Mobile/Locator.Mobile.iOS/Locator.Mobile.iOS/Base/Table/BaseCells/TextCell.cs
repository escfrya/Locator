using MonoTouch.UIKit;
using XibFree;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class TextCell : BaseCell<TextItem>
    {
        public TextCell(string identifier)
            : base(identifier)
        {
        }

        public UILabel Label { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            Layout.AddSubView(new NativeView
            {
                View = Label = new UILabel {BackgroundColor = UIColor.Clear},
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.WrapContent,
                }
            });
        }

        protected override void SetData(TextItem item)
        {
            Label.Text = item.Text;
            Label.Lines = item.Multiline ? 0 : 1;
        }
    }

    public class TextItem : Item
    {
        public string Text { get; set; }
        public bool Multiline { get; set; }
        public string TextClass { get; set; }

        public override BaseCell CreateCell()
        {
            return new TextCell(GetIdentifier());
        }
    }
}