using System;
using System.Collections.Generic;
using MonoTouch.UIKit;
using Locator.Mobile.iOS.Extentions;

namespace Locator.Mobile.iOS.Base.Table
{
    public class HeaderFooterItemSource : HeaderFooterItemSource<Item>
    {
        public HeaderFooterItemSource(IEnumerable<Item> items, Func<string, BaseSectionView> headerFactory,
            Func<string, BaseSectionView> footerFactory)
            : base(items, headerFactory, footerFactory)
        {
        }

        public HeaderFooterItemSource(List<Section> sections, Func<string, BaseSectionView> headerFactory,
            Func<string, BaseSectionView> footerFactory)
            : base(sections, headerFactory, footerFactory)
        {
        }
    }

    public class HeaderFooterItemSource<TItem> : HeaderItemSource<TItem>
        where TItem : Item
    {
        private readonly Func<string, BaseSectionView> _footerFactory;

        public HeaderFooterItemSource(IEnumerable<TItem> items, Func<string, BaseSectionView> headerFactory,
            Func<string, BaseSectionView> footerFactory)
            : base(items, headerFactory)
        {
            _footerFactory = footerFactory;
        }

        public HeaderFooterItemSource(List<Section> sections, Func<string, BaseSectionView> headerFactory,
            Func<string, BaseSectionView> footerFactory)
            : base(sections, headerFactory)
        {
            _footerFactory = footerFactory;
        }

        public override UIView GetViewForFooter(UITableView tableView, int section)
        {
            Section data = Sections[section];
            BaseSectionView view = tableView.DequeueReusableHeaderFooterView(data.ReuseFooterIdentifier.ToNSString()) as
                BaseSectionView
                                   ?? _footerFactory(data.ReuseFooterIdentifier);
            view.InitView(data);
            return view;
        }

        public override float GetHeightForFooter(UITableView tableView, int section)
        {
            var view = (BaseSectionView) GetViewForFooter(tableView, section);
            return view.CalculateHeight(tableView, Sections[section]);
        }
    }

    public class HeaderItemSource : HeaderItemSource<Item>
    {
        public HeaderItemSource(IEnumerable<Item> items, Func<string, BaseSectionView> headerFactory)
            : base(items, headerFactory)
        {
        }

        public HeaderItemSource(List<Section> sections, Func<string, BaseSectionView> headerFactory)
            : base(sections, headerFactory)
        {
        }
    }

    public class HeaderItemSource<TItem> : ItemSource<TItem>
        where TItem : Item
    {
        private readonly Func<string, BaseSectionView> _headerFactory;

        public HeaderItemSource(IEnumerable<TItem> items, Func<string, BaseSectionView> headerFactory) : base(items)
        {
            _headerFactory = headerFactory;
        }

        public HeaderItemSource(List<Section> sections, Func<string, BaseSectionView> headerFactory) : base(sections)
        {
            _headerFactory = headerFactory;
        }

        public override UIView GetViewForHeader(UITableView tableView, int section)
        {
            Section data = Sections[section];
            BaseSectionView view = tableView.DequeueReusableHeaderFooterView(data.ReuseHeaderIdentifier.ToNSString()) as
                BaseSectionView
                                   ?? _headerFactory(data.ReuseHeaderIdentifier);
            view.InitView(data);
            return view;
        }

        public override float GetHeightForHeader(UITableView tableView, int section)
        {
            var view = (BaseSectionView) GetViewForHeader(tableView, section);
            return view.CalculateHeight(tableView, Sections[section]);
        }

        public override string TitleForHeader(UITableView tableView, int section)
        {
            return null;
        }

        public override string TitleForFooter(UITableView tableView, int section)
        {
            return null;
        }
    }
}