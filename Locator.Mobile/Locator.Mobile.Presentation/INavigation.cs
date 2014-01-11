namespace Locator.Mobile.Presentation
{
    public interface INavigation
    {
        void Friends();

        void Locations();

        void OpenLocation(long locationId);

        void GoBack();
    }
}
