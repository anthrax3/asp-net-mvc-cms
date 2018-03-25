using asp_net_mvc_cms.Areas.Admin.ViewModels;
using asp_net_mvc_cms.Data;
using asp_net_mvc_cms.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace asp_net_mvc_cms.Areas.Admin.Services
{
    public class UserService
    {
        private readonly IUserRepository _users;
        private readonly IRoleRepository _roles;
        private readonly ModelStateDictionary _modelState;

        public UserService(ModelStateDictionary modelState,
            IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _modelState = modelState;
            _users = userRepository;
            _roles = roleRepository;
        }

        public async Task< bool> CreateAsync(UserViewModel model)
        {
            if (!_modelState.IsValid)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.NewPassword))
            {
                _modelState.AddModelError(string.Empty, "You must type a password.");
                return false;
            }

            var newUser = new CmsUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.UserName
            };

            await _users.CreateAsync(newUser, model.NewPassword);

            return true;
        }

        internal Task GetUserByNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        internal Task UpdateUser(UserViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}