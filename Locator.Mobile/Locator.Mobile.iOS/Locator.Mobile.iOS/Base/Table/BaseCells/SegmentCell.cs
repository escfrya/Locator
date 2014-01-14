using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using XibFree;
using Locator.Mobile.iOS.Extentions;

namespace Locator.Mobile.iOS.Base.Table.BaseCells
{
    public class SegmentCell : BaseCell<SegmentItem>
    {
        public SegmentCell(string identifier)
            : base(identifier)
        {
        }

        public UISegmentedControl SegmentedControl { get; private set; }

        public override void Initialize()
        {
            base.Initialize();

            SegmentedControl = new UISegmentedControl();
            SegmentedControl.ValueChanged += (x, e) => SelectSegment();

            Layout.AddSubView(new NativeView
            {
                View = SegmentedControl,
                LayoutParameters = new LayoutParameters
                {
                    Width = AutoSize.FillParent,
                    Height = DeviceInfo.IOS6 ? 29f : AutoSize.WrapContent,
                },
                Init = c => c.As<UISegmentedControl>().Setup(SetupSegmentedControl)
            });
        }

        protected override void SetData(SegmentItem item)
        {
            int pos = 0;
            SegmentedControl.RemoveAllSegments();
            foreach (string segment in item.Segments)
            {
                SegmentedControl.InsertSegment(segment, pos++, false);
            }
            SegmentedControl.SelectedSegment = item.SelectedSegment;
        }

        private void SelectSegment()
        {
            if (CurrentItem != null)
            {
                CurrentItem.SelectedSegment = SegmentedControl.SelectedSegment;
            }
        }

        private static void SetupSegmentedControl(UISegmentedControl sc)
        {
            if (!DeviceInfo.IOS6) return;
            sc.Layer.BorderWidth = 1;
            sc.Layer.BorderColor = UIColor.FromRGB(0, 122, 255).CGColor;
            sc.Layer.CornerRadius = 4;
            sc.Layer.MasksToBounds = true;
        }
    }


    public class SegmentItem : Item
    {
        private int _selectedSegment;

        public List<string> Segments { get; set; }

        public int SelectedSegment
        {
            get { return _selectedSegment; }
            set
            {
                if (_selectedSegment == value) return;
                _selectedSegment = value;
                Action call = SelectedSegmentChanged;
                if (call != null)
                    call();
            }
        }

        public override BaseCell CreateCell()
        {
            return new SegmentCell(GetIdentifier());
        }

        public event Action SelectedSegmentChanged;
    }
}