﻿


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Locator.Mobile.BL.Request
{
	public class BaseRequest
	{
	}

	public class SendLocationRequest : BaseRequest
	{
		public Locator.Entity.Entities.Location location { get; set; }
	}

	public class AddUserRequest : BaseRequest
	{
		public Locator.Entity.Entities.User user { get; set; }
	}

	public class UpdateUserPushRequest : BaseRequest
	{
		public Locator.Entity.Entities.UserPush userPush { get; set; }
	}
}