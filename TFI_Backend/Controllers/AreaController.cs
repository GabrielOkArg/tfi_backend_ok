using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFI_Backend.Application.Area.Create;
using TFI_Backend.Application.Area.GetAll;
using TFI_Backend.Application.Area.Update;

namespace TFI_Backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreaController : ControllerBase
    {

        private readonly GetAreaQueryHandler getAreaQueryHandler;
        private readonly CreateAreaCommandHandler createAreaCommandHandler;
        private readonly UpdateAreaCommandHandler updateAreaCommandHandler;

        public AreaController(GetAreaQueryHandler getAreaQueryHandler,
            CreateAreaCommandHandler createAreaCommandHandler
            , UpdateAreaCommandHandler updateAreaCommandHandler
            )
        {
            this.getAreaQueryHandler = getAreaQueryHandler;
            this.createAreaCommandHandler = createAreaCommandHandler;
            this.updateAreaCommandHandler = updateAreaCommandHandler;
        }



        [HttpGet("getall")]
        public async Task<IActionResult> GetAreas()
        {
            var query = new GatAreasQuery();
            query.UserID = int.Parse(User.FindFirst("id")!.Value);
            var result = await getAreaQueryHandler.Handle(query);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateArea([FromBody]CreateAreaCommand command )
        {
            var result = await createAreaCommandHandler.Handle(command);
            if (result.IsOk)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateAreaCommand command)
        {
            var result = await updateAreaCommandHandler.Handle(command);
            if (result.IsOk)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
