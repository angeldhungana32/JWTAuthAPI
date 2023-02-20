using JWTAuthAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthAPI.Controllers.v1
{
    [Route(RoutesConstant.DefaultControllerRoutev1)]
    [ApiController]
    [Authorize]
    public class V1BaseController : ControllerBase
    {
    }
}
