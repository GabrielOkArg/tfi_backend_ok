using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Interfaces;

namespace TFI_Backend.Application.Reclamos.Update
{
    public class UpdateReclamoCommandHandler
    {

        private readonly IReclamoRepository _repository;
        private readonly IUserContext _userContext;
        private readonly IreclamoPresupuesto _reclamoPresupuesto;

        public UpdateReclamoCommandHandler(IReclamoRepository repository, IUserContext userContext, IreclamoPresupuesto reclamoPresupuesto)
        {
            _repository = repository;
            _userContext = userContext;
            _reclamoPresupuesto = reclamoPresupuesto;
        }
        public async Task<UpdateReclamoCommandResponse> Handle(UpdateReclamoCommand command)
        {
            var currentUserId = _userContext.GetUserId();

            // 1. Validar que coincide el usuario del front con el token
            if (command.UsuarioId != currentUserId)
                throw new UnauthorizedAccessException("El usuario no coincide con el usuario logueado.");

            // 2. Obtener el reclamo
            var reclamo = _repository.GetById(command.Id);

            if (reclamo == null)
                throw new Exception("El reclamo no existe.");
           
                var presupuesExistente = _reclamoPresupuesto.GetAll().Where(re => re.ReclamoId == command.Id).FirstOrDefault();
            

            // 3. Validar que el reclamo pertenece al usuario logueado
            //if (reclamo.UsuarioId != currentUserId)
            //    throw new UnauthorizedAccessException("No tienes permiso para modificar este reclamo.");

            // 4. Actualizar campos
            reclamo.Titulo = command.Tipo;
            reclamo.Descripcion = command.Descripcion;
            reclamo.Costo = command.Costo ?? reclamo.Costo;
            reclamo.Estado = command.Estado;
            if (presupuesExistente == null)
            {
                if (command.PresupuestoRuta != "" && command.PresupuestoRuta != null)
                {
                    reclamo.Presupuesto = new Core.Model.ReclamoPresupuesto
                    { Ruta = command.PresupuestoRuta };
                }
            }


            // 5. Guardar
            _repository.Update(reclamo);
            _repository.SaveChanges();

            return new UpdateReclamoCommandResponse
            {
                Id = reclamo.Id,
                Tipo = reclamo.Titulo,
                Descripcion = reclamo.Descripcion,

            };
        }
    }
}
