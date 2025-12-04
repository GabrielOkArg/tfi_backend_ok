using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Application.Reclamos
{
    public class CreateReclamoHandler
    {

        private readonly IReclamoRepository _reclamoRepository;

        public CreateReclamoHandler(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        public async Task<CreateReclamoResponse> Handle(CreateReclamoCommand command)
        {
            var reclamo = new Reclamo
            {
                Titulo = command.Titulo,
                Descripcion = command.Descripcion,
                UsuarioId = command.UsuarioId,
                FechaCreacion = DateTime.Now,
                AreaId = command.AreaId,
                Estado = "Pendiente"
            };

            foreach (var ruta in command.ImagenRutas)
            {
                reclamo.Imagenes.Add(new ReclamoImagen
                {
                    Ruta = ruta
                });
            }

            await _reclamoRepository.AddAsync(reclamo);
            await _reclamoRepository.SaveChangesAsync();

            return new CreateReclamoResponse
            {
                Id = reclamo.Id,
                Titulo = reclamo.Titulo,
                Estado = reclamo.Estado,
                FechaCreacion = reclamo.FechaCreacion
            };
        }
    }
}
