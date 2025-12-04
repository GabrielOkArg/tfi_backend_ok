using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Application.Area.Update
{
    public class UpdateAreaCommandHandler
    {

        private readonly IAreaRepository _areaRepository;
        public UpdateAreaCommandHandler(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }
        public async Task<UpdateAreaCommandResponse> Handle(UpdateAreaCommand command)
        {
            try
            {
                var area = _areaRepository.GetAll().Where(a => a.Id == command.Id).FirstOrDefault();
                if (area == null)
                {
                    return new UpdateAreaCommandResponse
                    {
                        Id = command.Id,
                        IsOk = false,
                        Message = "El área que intenta modificar no existe."
                    };
                }

                area.Nombre = command.Nombre;
                 _areaRepository.Update(area);
                await _areaRepository.SaveChangesAsync();
                return new UpdateAreaCommandResponse
                {
                    Id = area.Id,
                    IsOk = true,
                    Message = "Área actualizada correctamente."
                };


            }
            catch (Exception)
            {
                return new UpdateAreaCommandResponse
                {
                    Id = 0,
                    IsOk = false,
                    Message = "Error al actualiar el Area."
                };

            }
        }
    }
}
