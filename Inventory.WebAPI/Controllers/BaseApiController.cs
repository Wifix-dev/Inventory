
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;

namespace Inventory.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
       protected readonly IMapper _mapper;
       public BaseApiController(IMapper mapper)
       {
            _mapper=mapper;
       }
    }
}