using System.Data.Entity;

namespace Locator.DAL.EF
{
    public class LocatorInitializer : DropCreateDatabaseAlways<LocatorContext>
    {
        protected override void Seed(LocatorContext context)
        {
        }

    }
}
