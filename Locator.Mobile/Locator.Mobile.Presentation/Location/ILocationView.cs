using System;
using Locator.Entity.Entities;

namespace Locator.Mobile.Presentation
{
    public interface ILocationView : IBaseView
    {
        long LocationId { get; set; }
        Location Location { get; set; }

        event Action<long> Refresh;

        void Update(Location location);
    }
}
