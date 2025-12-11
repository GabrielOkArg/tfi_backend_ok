using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Interfaces;

namespace TFI_Backend.Application.Reclamos.GetReclamos
{

   
    public class GetReclamosCommandHandler
    {
        private readonly IReclamoRepository _reclamoRepository;
        public GetReclamosCommandHandler(IReclamoRepository reclamoRepository)
        {
            _reclamoRepository = reclamoRepository;
        }

        public async Task<List<GetReclamosCommandResponse>> Handle(GetReclamosCommand command)
        {
            try
            {
                var reclamos = _reclamoRepository.GetAll();
                var response = reclamos.Select(r => new GetReclamosCommandResponse
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Descripcion = r.Descripcion,
                    Estado = r.Estado,
                    FechaCreacion = r.FechaCreacion,
                    UsuarioId = r.UsuarioId,
                    UsuarioNombre = r.Usuario?.Nombre,
                    Costo = r.Costo,
                    AreaId = r.AreaId,
                    AreaNombre = r.Area?.Nombre,
                    Imagenes = r.Imagenes.Select(i => i.Ruta).ToList(),
                    Presupuesto = r.Presupuesto !=null? r.Presupuesto.Ruta : "",
                    FechaFin = (DateTime)r.FechaFin,
                    ComentarioTecnico = r.ComentarioTecnico

                }).ToList();

                return await Task.FromResult(response);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
