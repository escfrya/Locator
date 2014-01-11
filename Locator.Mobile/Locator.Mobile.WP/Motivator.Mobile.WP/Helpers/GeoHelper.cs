using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Motivator.Mobile.WP.Helpers
{
    public class GeoHelper
    {
        private readonly Geolocator geolocator;
        public GeoHelper()
        {
            geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.High };
        }

        public async Task<Geoposition> GetGeoPosition()
        {
            return await geolocator.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(10));
        }
    }
}
