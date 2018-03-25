using asp_net_mvc_cms.Areas.Admin.Services;
using asp_net_mvc_cms.Areas.Admin.ViewModels;
using asp_net_mvc_cms.Data;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace asp_net_mvc_cms.Areas.Admin.Controllers
{
    [RouteArea("admin")]
    [RoutePrefix("users")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly UserService _users;

        public UsersController()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _users = new UserService(ModelState, _userRepository, _roleRepository);
        }

        // GET: Admin/Users
        [Route("")]
        public async Task<ActionResult> Index()
        {
            return View(await _userRepository.GetAllUsersAsync());
        }

        [HttpGet]
        [Route("create")]
        public async Task<ActionResult> Create()
        {
            var model = new UserViewModel();

            return View(model);
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            var completed = await _users.CreateAsync(model);

            if (completed)
            {
                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpGet]
        [Route("edit/{username}")]
        public async Task<ActionResult> Edit(string username)
        {
            var user = await _users.GetUserByNameAsync(username);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [Route("edit/{username}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            //using (var userStore = new CmsUserStore())
            //using (var userManager = new CmsUserManager(userStore))
            //{
            //    var user = userStore.FindByNameAsync(model.UserName).Result;

            //    if (user == null)
            //    {
            //        return HttpNotFound();
            //    }

            //    if (!ModelState.IsValid)
            //    {
            //        return View(model);
            //    }

            //    if (!string.IsNullOrWhiteSpace(model.NewPassword))
            //    {
            //        if (string.IsNullOrWhiteSpace(model.CurrentPassword))
            //        {
            //            ModelState.AddModelError(string.Empty, "The current password must be supplied.");
            //            return View(model);
            //        }

            //        var passwordVerified = userManager.PasswordHasher.VerifyHashedPassword(user.PasswordHash, model.CurrentPassword);

            //        if (passwordVerified != PasswordVerificationResult.Success)
            //        {
            //            ModelState.AddModelError(string.Empty, "The current password does not match our records.");
            //            return View(model);
            //        }

            //        var newHashedPassword = userManager.PasswordHasher.HashPassword(model.NewPassword);

            //        user.PasswordHash = newHashedPassword;
            //    }

            //    user.Email = model.Email;
            //    user.DisplayName = model.DisplayName;

            //    var updateResult = userManager.UpdateAsync(user).Result;

            //    if (updateResult.Succeeded)
            //    {
            //        return RedirectToAction("index");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError(string.Empty, "An error occurred. Please, try again.");
            //        return View(model);
            //    }
            //}

            var userUpdated = await _users.UpdateUser(model);

            if (userUpdated)
            {
                return RedirectToAction("index");
            }

            return View(model);
        }

        [HttpPost]
        [Route("delete/{username}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string username)
        {
            using (var userStore = new CmsUserStore())
            using (var userManager = new CmsUserManager(userStore))
            {
                var user = userStore.FindByNameAsync(username).Result;

                if (user == null)
                {
                    return HttpNotFound();
                }

                var deleteResult = userManager.DeleteAsync(user).Result;

                return RedirectToAction("index");
            }
        }

        private bool _isDisposed;
        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _userRepository.Dispose();
                _roleRepository.Dispose();
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}