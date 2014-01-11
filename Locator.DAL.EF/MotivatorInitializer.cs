using System.Data.Entity;

namespace Locator.DAL.EF
{
    public class LocatorInitializer : DropCreateDatabaseIfModelChanges<LocatorContext>
    {
        protected override void Seed(LocatorContext context)
        {
        }

    }
}
