using System.Collections.Generic;

namespace Locator.Mobile.iOS.Base.Table
{
    public abstract class Item
    {
        public abstract BaseCell CreateCell();

        public virtual string GetIdentifier()
        {
            return GetType().FullName;
        }
    }

    public class Section
    {
        public Section()
        {
            Items = new List<Item>();
        }

        public List<Item> Items { get; set; }

        public string Header { get; set; }
        public string Footer { get; set; }
        public string ReuseHeaderIdentifier { get; set; }
        public string ReuseFooterIdentifier { get; set; }
    }
}