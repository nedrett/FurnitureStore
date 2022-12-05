namespace FurnitureStore.Areas.Admin.Controllers
{
    using Core.Contracts.User;
    using Microsoft.AspNetCore.Mvc;

    public class UserController : AdminController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [Route("User/All")]
        public async Task<IActionResult> All()
        {
            var users = await userService.All();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Forget(string id)
        {
            await userService.Forget(id);

            return RedirectToAction(nameof(All));
        }
    }
}
