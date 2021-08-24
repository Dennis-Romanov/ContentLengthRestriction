using Microsoft.AspNetCore.Mvc;

namespace ContentRestriction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public string Test()
        {
            return "So far so good!";
        }
    }
}