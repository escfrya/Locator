using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Locator.Mobile.iOS.Base.Table
{
    public class ItemSource : ItemSource<Item>
    {
        public ItemSource(IEnumerable<Item> items) : base(items)
        {
        }

        public ItemSource(List<Section> sections) : base(sections)
        {
        }
    }


    public class ItemSource<TItem> : UITableViewSource
        where TItem : Item
    {
        protected readonly List<Section> Sections;

        private readonly Dictionary<string, BaseCell> _prototypes = new Dictionary<string, BaseCell>();

        public ItemSource(IEnumerable<TItem> items)
            : this(new List<Section> {new Section {Items = items.ToList<Item>()}})
        {
        }

        public ItemSource(List<Section> sections)
        {
            Sections = sections;
            AllowSelectRow = true;
            UnselectRows = true;
        }

        public Action<TItem> Touch { get; set; }
        public bool AllowSelectRow { get; set; }
        public bool UnselectRows { get; set; }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return Sections[section].Items.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            List<Item> items = Sections[indexPath.Section].Items;
            Item data = items[indexPath.Row];
            string identifier = data.GetIdentifier();
            var cell = tableView.DequeueReusableCell(identifier) as BaseCell;
            if (cell == null)
            {
                cell = data.CreateCell();
                cell.Initialize();
            }
            cell.SetCellData(data);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            List<Item> items = Sections[indexPath.Section].Items;
            if (UnselectRows)
            {
                tableView.DeselectRow(indexPath, true);
            }
            Action<TItem> call = Touch;
            Item item = items[indexPath.Row];
            if (call != null)
                call((TItem) item);
        }

        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            List<Item> items = Sections[indexPath.Section].Items;
            Item data = items[indexPath.Row];
            string identifier = data.GetIdentifier();
            BaseCell cell;

            if (!_prototypes.TryGetValue(identifier, out cell))
            {
                _prototypes[identifier] = cell = data.CreateCell();
                cell.Initialize();
            }

            return cell.CalculateHeight(tableView, data);
        }

        public override int NumberOfSections(UITableView tableView)
        {
            return Sections.Count;
        }

        public override string TitleForHeader(UITableView tableView, int section)
        {
            return Sections[section].Header;
        }

        public override string TitleForFooter(UITableView tableView, int section)
        {
            return Sections[section].Footer;
        }

        public override bool ShouldHighlightRow(UITableView tableView, NSIndexPath rowIndexPath)
        {
            return AllowSelectRow;
        }
    }
}