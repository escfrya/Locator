using System.Device.Location;
using Windows.Devices.Geolocation;

namespace Motivator.Mobile.WP
{
    public static class Extensions
    {
        public static GeoCoordinate ToGeoCoordinate(this Geocoordinate coord)
        {
            return new GeoCoordinate(coord.Latitude, coord.Longitude);
        }
    }
}
