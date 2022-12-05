using Microsoft.AspNetCore.Mvc;
using static FurnitureStore.Areas.Admin.AdminConstants;

namespace FurnitureStore.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRoleName)]
    public class AdminController : Controller
    {
        
    }
}
