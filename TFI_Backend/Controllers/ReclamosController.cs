using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using TFI_Backend.Application.Reclamos;
using TFI_Backend.Application.Reclamos.GatById;
using TFI_Backend.Application.Reclamos.GetByUserId;
using TFI_Backend.Application.Reclamos.GetReclamos;
using TFI_Backend.Application.Reclamos.Update;
using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;
using TFI_Backend.DTO;
using TFI_Backend.Infrastructure.Repositories;
using TFI_Backend.Infrastructure.Services;



namespace TFI_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReclamosController : ControllerBase
    {
        private readonly CreateReclamoHandler _createHandler;
        private readonly GetByUserIdQueryHandler _handler;
        private readonly GetReclamoByIdQueryHandler _getbyidHandler;
        private readonly UpdateReclamoCommandHandler _updateHandler;
        private readonly GetReclamosCommandHandler _getReclamosHandler;

        public ReclamosController(CreateReclamoHandler createHandler,
            GetByUserIdQueryHandler handler
            ,
            GetReclamoByIdQueryHandler getbyidHandler,
            UpdateReclamoCommandHandler updateHandler,
            GetReclamosCommandHandler getReclamosHandler)
        {
            _createHandler = createHandler;
            _handler = handler;
            _getbyidHandler = getbyidHandler;
            _updateHandler = updateHandler;
            _getReclamosHandler = getReclamosHandler;
        }

        //[HttpPost]
        //public async Task<IActionResult> CrearReclamo([FromForm] CreateReclamoDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var imagePaths = new List<string>();

        //    if (dto.Imagenes != null)
        //    {
        //        foreach (var img in dto.Imagenes)
        //        {
        //            var fileName = $"{Guid.NewGuid()}_{img.FileName}";
        //            var path = Path.Combine("C:\\projects\\CUDI\\TFI\\wwwroot\\reclamos", fileName);

        //            using (var stream = new FileStream(path, FileMode.Create))
        //            {
        //                await img.CopyToAsync(stream);
        //            }

        //            imagePaths.Add(path);
        //        }
        //    }

        //    var command = new CreateReclamoCommand
        //    {
        //        Titulo = dto.Titulo,
        //        Descripcion = dto.Descripcion,
        //        UsuarioId = dto.UsuarioId,
        //        AreaId = dto.AreaId,
        //        ImagenRutas = imagePaths
        //    };

        //    var result = await _createHandler.Handle(command);
        //    return Ok(result);
        //}
        [HttpPost]
        public async Task<IActionResult> CrearReclamo([FromForm] CreateReclamoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imagePaths = new List<string>();

            if (dto.Imagenes != null)
            {
                foreach (var img in dto.Imagenes)
                {
                    var fileName = $"{Guid.NewGuid()}_{img.FileName}";

                    // RUTA FÍSICA EN EL SERVIDOR (solo para FileStream)
                    var physicalPath = Path.Combine(
                        "C:\\projects\\CUDI\\TFI\\wwwroot\\reclamos",
                        fileName
                    );

                    using (var stream = new FileStream(physicalPath, FileMode.Create))
                    {
                        await img.CopyToAsync(stream);
                    }

                    // RUTA PÚBLICA PARA EL FRONTEND Y LA BD
                    var publicUrl = $"wwwroot/reclamos/{fileName}";

                    imagePaths.Add(publicUrl);
                }
            }

            var command = new CreateReclamoCommand
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                UsuarioId = dto.UsuarioId,
                AreaId = dto.AreaId,
                ImagenRutas = imagePaths
            };

            var result = await _createHandler.Handle(command);
            return Ok(result);
        }


        [HttpPost("getByUserId")]
        [Authorize]
        public async Task<IActionResult> GetByUserID([FromBody] GetByUserIdQuery command)
        {
            var result = await _handler.Handle(command);
            return Ok(result);
        }

        [HttpGet("getById")]
        [Authorize]
        public async Task<IActionResult> GetById([FromQuery] int reclamoId, [FromQuery] int usuarioId)
        {
            var command = new GetReclamoByIdQueryCommand
            {
                ReclamoId = reclamoId,
                UsuarioId = usuarioId
            };

            var result = await _getbyidHandler.Handle(command);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        //[HttpPut("Update")]
        //[Authorize]
        //public async Task<IActionResult> Update([FromBody] UpdateReclamoCommand command)
        //{


        //    var response = await _updateHandler.Handle(command);

        //    return Ok(response);
        //}
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateReclamoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string? presupuestoRuta = null;

            if (dto.PresupuestoArchivo != null)
            {
                var file = dto.PresupuestoArchivo;

                var fileName = $"{Guid.NewGuid()}_{file.FileName}";

                // Ruta física donde se guarda
                var physicalPath = Path.Combine(
                    "C:\\projects\\CUDI\\TFI\\wwwroot\\reclamos",
                    fileName
                );

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Ruta a guardar en DB
                presupuestoRuta = $"wwwroot/reclamos/{fileName}";
            }

            var command = new UpdateReclamoCommand
            {
                Id = dto.Id,
                Tipo = dto.Tipo,
                Descripcion = dto.Descripcion,
                UsuarioId = dto.UsuarioId,
                Reclamo = dto.Reclamo,
                Costo = dto.Costo,
                Estado = dto.Estado,
                PresupuestoRuta = presupuestoRuta
            };

            var response = await _updateHandler.Handle(command);

            return Ok(response);
        }



        [HttpGet("reclamos")]
        public async Task<IActionResult> GetReclamos()
        {
            int usuarioId = int.Parse(User.FindFirst("id")!.Value);
            GetReclamosCommand command = new GetReclamosCommand
            {
                UsuarioId = usuarioId
            };
            var result = await _getReclamosHandler.Handle(command);
            return Ok(result);
        }


    }
}
