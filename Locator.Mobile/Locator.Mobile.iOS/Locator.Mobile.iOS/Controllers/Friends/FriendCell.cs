using System;
using MonoTouch.UIKit;
using XibFree;
using Locator.Mobile.iOS.Extentions;
using Locator.Mobile.iOS.Base.Table;
using Locator.Entity.Entities;

namespace Locator.Mobile.iOS.Controllers.Friends
{
	public class FriendCell : BaseCell<FriendItem>
    {
		private static readonly UIFont Font = UIFont.FromName("Arial", 21);
		private UILabel _friendLabel;

		public FriendCell(string identifier)
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
					/*new NativeView
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
						View = _friendLabel = new UILabel {Lines = 0, Font = Font, BackgroundColor = UIColor.Clear},
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

		protected override void SetData(FriendItem item)
        {
			//_friendLabel.Font = UIFont.FromName("Arial", item.GetFontSize());
			_friendLabel.Text = item.Friend.DisplayName;
        }

        private static void ContentStyle(UILabel obj)
        {
			obj.Font = UIFont.FromName("Arial", 21);
            obj.TextColor = UIColorExtentions.Gray(71);
        }
    }

	public class FriendItem : Item
    {
		public FriendItem(User friend)
        {
			Friend = friend;
        }

		public User Friend { get; private set; }

        public override BaseCell CreateCell()
        {
			return new FriendCell(GetIdentifier());
        }
    }
}