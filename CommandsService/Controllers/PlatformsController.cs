using AutoMapper;
using CommandsService.Data;
using CommandsService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
       {
            _repository = repository;
            _mapper = mapper;
        }
       
       [HttpGet]
       public ActionResult<IEnumerable<PlatformreadDto>> GetPlatforms()
       {
           Console.WriteLine("--> Getting platforms from commandsService");

           var platformsItems = _repository.GetAllPlatforms();
           return Ok(_mapper.Map<IEnumerable<PlatformreadDto>>(platformsItems));
       }

       [HttpPost]
       public ActionResult TestInBoundConnection()
       {
        Console.WriteLine("--> Inbound Post # Command Service");
        return Ok("Inbound test of from platforms controller");
       }
    }
}