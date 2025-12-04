using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Application.Area.Create
{
    public class CreateAreaCommandHandler
    {

        private readonly IAreaRepository areaRepository;

        public CreateAreaCommandHandler(IAreaRepository areaRepository)
        {
            this.areaRepository = areaRepository;
        }


        public async Task<CreateAreaCommandResponse> Handle(CreateAreaCommand command)
        {
            try
            {
                var area = new TFI_Backend.Core.Model.Area
                {
                    Nombre = command.Nombre,
                    ParentAreaId = command.ParentAreaId
                };
                await areaRepository.AddAsync(area);
                await areaRepository.SaveChangesAsync();
                return new CreateAreaCommandResponse
                {
                    Id = area.Id,
                    Messaje="Area creada correctamente",
                    IsOk= true
                };
            }
            catch (Exception)
            {

                return new CreateAreaCommandResponse
               {
                   Id = 0,
                   Messaje="Error al crear el area",
                     IsOk= false
                };
            }
        }
    }
}
