using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Motivator.DAL.EF;
using Motivator.DAL.Repositories;
using Motivator.Entity.Entities;
using MotivatorServiceWebRole.Authorization;
using MotivatorServiceWebRole.Cache;

namespace MotivatorServiceWebRole
{
    [ServiceBehavior(
    ConcurrencyMode = ConcurrencyMode.Single,
    InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class MotivatorService : IMotivatorService
    {
        private readonly IMotivActionRepository motivActionRepository;
        private readonly IUserRepository userRepository;

        public MotivatorService()
        {
            var context = new MotivatorContext();
            motivActionRepository = new MotivActionRepository(context);
            userRepository = new UserRepository(context);
        }

        [Authorization]
        [Cache(0)]
        public List<MotivAction> GetActions()
        {
            return motivActionRepository.GetAll().ToList();
        }

        [Authorization]
        [Cache(0)]
        public MotivAction GetAction(string actionId)
        {
            return motivActionRepository.Get(int.Parse(actionId));
        }

        [Authorization]
        public MotivAction AddAction(MotivAction action)
        {
            action.UserID = ((CustomUser)HttpContext.Current.User).CurrentUser.ID;
            action.CreatedDate = DateTime.Now;
            return motivActionRepository.Add(action);
        }

        [Authorization]
        public MotivAction UpdateAction(MotivAction action)
        {
            var entity = motivActionRepository.Get(action.ID);
            entity.ActionDate = action.ActionDate;
            entity.Success = action.Success;
            entity.Text = action.Text;
            motivActionRepository.Update(entity);
            return entity;
        }

        public User AddUser(User user)
        {
            return userRepository.Add(user);
        }
    }
}
