using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Interfaces;

namespace TFI_Backend.Application.Reclamos.GetByUserId
{
    public class GetByUserIdQueryHandler
    {

        private readonly IReclamoRepository _reclamoRepo;
        private readonly IUserContext _userContext;

        public GetByUserIdQueryHandler(
            IReclamoRepository reclamoRepo,
            IUserContext userContext)
        {
            _reclamoRepo = reclamoRepo;
            _userContext = userContext;
        }

        public async Task<IEnumerable<GetByUserIdQueryResponse>> Handle(GetByUserIdQuery command)
        {
            // 🔥 ID del usuario autenticado según JWT
            var usuarioActualId = _userContext.GetUserId();

            // 🔥 Validación crítica
            if (usuarioActualId != command.UsuarioId)
                throw new UnauthorizedAccessException("El usuario no coincide con el token");

            // 🔍 Obtener reclamos del usuario
            var reclamos = _reclamoRepo.GetByUsuarioId(command.UsuarioId);

            return reclamos.Select(r => new GetByUserIdQueryResponse
            {
                Id = r.Id,
                Titulo = r.Titulo,
                Descripcion = r.Descripcion,
                FechaCreacion = r.FechaCreacion,
                UsuarioId = r.UsuarioId,
                Estado = r.Estado

            });
        }
    }
}
