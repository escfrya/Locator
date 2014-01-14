using System;
using Locator.Mobile.iOS.Base.Table;
using MonoTouch.UIKit;
using XibFree;
using Locator.Mobile.iOS.Extentions;
using Locator.Entity.Entities;

namespace Locator.Mobile.iOS
{
	public class LocationCell : BaseCell<LocationItem>
	{
		private static readonly UIFont Font = UIFont.FromName("Arial", 21);
		private UILabel _locationLabel;

		public LocationCell(string identifier)
			: base(identifier)
		{
		}

		public override void Initialize()
		{
			base.Initialize();

			SeparatorInset = new UIEdgeInsets(0, 0, 0, 0);

			Layout = new FrameLayout
			{
				Padding = new UIEdgeInsets(0, 9, 0, 12),
				LayoutParameters = new LayoutParameters
				{
					Width = AutoSize.FillParent,
					Height = AutoSize.WrapContent,
				},
				SubViews = new View[]
				{
					/*					new NativeView
                    {
                        View = new UIImageView(Assets.Bubble()),
                        LayoutParameters = new LayoutParameters
                        {
                            Width = AutoSize.FillParent,
                            Height = AutoSize.FillParent
                        }
                    },*/
					new NativeView
					{
						View = _locationLabel = new UILabel {Lines = 0, Font = Font, BackgroundColor = UIColor.Clear},
						LayoutParameters = new LayoutParameters
						{
							Width = AutoSize.FillParent,
							Height = AutoSize.WrapContent,
							Margins = new UIEdgeInsets(10, 18, 10, 10)
						},
						Init = v => v.As<UILabel>().Setup(ContentStyle)
					}
				}
			};

			ContentView.Add(new UILayoutHost(Layout, ContentView.Bounds));
			Accessory = UITableViewCellAccessory.None;
		}

		protected override void SetData(LocationItem item)
		{
			_locationLabel.Text = item.Location.Description;
		}

		private static void ContentStyle(UILabel obj)
		{
			obj.Font = UIFont.FromName("Arial", 21);
			obj.TextColor = UIColorExtentions.Gray(71);
		}
	}

	public class LocationItem : Item
	{
		public LocationItem(Location location)
		{
			Location = location;
		}

		public Location Location { get; private set; }

		public override BaseCell CreateCell()
		{
			return new LocationCell(GetIdentifier());
		}
	}
}

