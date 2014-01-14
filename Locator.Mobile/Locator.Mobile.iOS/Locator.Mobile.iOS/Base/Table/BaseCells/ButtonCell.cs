using System;
using MonoTouch.UIKit;
using XibFree;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class ButtonCell : BaseCell<ButtonItem>
    {
        public ButtonCell(string identifier)
            : base(identifier)
        {
        }

        public UIButton Button { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            Button = UIButton.FromType(UIButtonType.System);
            Button.TouchUpInside += (x, e) => InvokeAction();

            Layout.AddSubView(new NativeView
            {
                View = Button,
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = AutoSize.WrapContent,
                }
            });
        }

        protected override void SetData(ButtonItem item)
        {
            Button.SetTitle(item.Text, UIControlState.Normal);
        }

        private void InvokeAction()
        {
            if (CurrentItem != null && CurrentItem.Action != null)
            {
                CurrentItem.Action();
            }
        }
    }

    public class ButtonItem : Item
    {
        public string Text { get; set; }
        public Action Action { get; set; }

        public override BaseCell CreateCell()
        {
            return new ButtonCell(GetIdentifier());
        }
    }
}