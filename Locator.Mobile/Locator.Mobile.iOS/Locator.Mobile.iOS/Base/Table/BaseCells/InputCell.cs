using MonoTouch.UIKit;
using XibFree;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class InputCell : BaseCell<InputItem>
    {
        public InputCell(string identifier)
            : base(identifier)
        {
        }

        public UITextField TextField { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            TextField = new UITextField();
            TextField.EditingChanged += (x, e) => ChangeText();

            Layout.AddSubView(new NativeView
            {
                View = TextField,
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.WrapContent,
                }
            });
        }

        protected override void SetData(InputItem item)
        {
            TextField.Text = item.Text;
        }

        private void ChangeText()
        {
            if (CurrentItem != null)
            {
                CurrentItem.Text = TextField.Text;
            }
        }
    }

    public class InputItem : Item
    {
        public string Text { get; set; }

        public override BaseCell CreateCell()
        {
            return new InputCell(GetIdentifier());
        }
    }
}