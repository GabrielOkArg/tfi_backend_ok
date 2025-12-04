using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Interfaces;

namespace TFI_Backend.Application.Reclamos.GatById
{
    public class GetReclamoByIdQueryHandler
    {

        private readonly IReclamoRepository _repo;
        private readonly IUserContext _userContext;

        public GetReclamoByIdQueryHandler(
            IReclamoRepository repo,
            IUserContext userContext)
        {
            _repo = repo;
            _userContext = userContext;
        }

        public async Task<GetReclamoByIdQueryResponse> Handle(GetReclamoByIdQueryCommand query)
        {
            var userIdFromToken = _userContext.GetUserId();

            // Validamos que el usuario del front coincida con el del token
            if (userIdFromToken != query.UsuarioId)
                throw new UnauthorizedAccessException("Usuario inválido.");

            var reclamo = _repo.GetById(query.ReclamoId);

            if (reclamo == null)
                return null;

            // Validamos que el reclamo pertenezca al usuario
            if (reclamo.UsuarioId != query.UsuarioId)
                throw new UnauthorizedAccessException("El reclamo no pertenece al usuario.");

            return new GetReclamoByIdQueryResponse
            {
                Reclamo = reclamo
            };
        }
    }
}
