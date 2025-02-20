using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
       public PlatformsController()
       {
        
       }
       
       [HttpPost]
       public ActionResult TestInBoundConnection()
       {
        System.Console.WriteLine("--> Inbound Post # Command Service");
        return Ok("Inbound test of from platforms controller");
       }
    }
}