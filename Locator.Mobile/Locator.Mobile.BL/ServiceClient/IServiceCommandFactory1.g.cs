﻿


//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Locator.Mobile.BL.Request;
using Locator.Mobile.BL.Response;
namespace Locator.Mobile.BL.ServiceClient
{
	public interface IServiceCommandFactory
	{
		BaseServiceCommand<Locator.ServiceContract.Models.LocationsModel> GetLocations();
		BaseServiceCommand<Locator.ServiceContract.Models.FriendsModel> GetFriends();
		BaseServiceCommand<BaseResponse> SendLocation(SendLocationRequest request);
		BaseServiceCommand<Locator.Entity.Entities.User> AddUser(AddUserRequest request);
		BaseServiceCommand<BaseResponse> UpdateUserPush(UpdateUserPushRequest request);
	}
}