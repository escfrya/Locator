//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Locator.DAL.EF;
using Locator.DAL.EF.Repositories;
using Locator.Entity.Entities;

namespace Locator.DAL.Repositories
{
	/// <summary>
	/// Location Repository
	/// </summary>
	public partial class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public  LocationRepository(LocatorContext context) : base(context)
        {
        }
    }
}
		