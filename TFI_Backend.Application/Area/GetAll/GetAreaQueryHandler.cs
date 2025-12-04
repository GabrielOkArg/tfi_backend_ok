using TFI_Backend.Core.Interfaces;

namespace TFI_Backend.Application.Area.GetAll
{
    public class GetAreaQueryHandler
    {
        private readonly IAreaRepository _areaRepository;

        public GetAreaQueryHandler(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<List<GetAreaQueryResponse>> Handle(GatAreasQuery request)
        {
            var areas = _areaRepository.GetAll().ToList();

            // Solo áreas raíz
            var rootAreas = areas.Where(a => a.ParentAreaId == null).ToList();

            var response = rootAreas
                .Select(area => MapArea(area))
                .ToList();

            return await Task.FromResult(response);
        }

        private GetAreaQueryResponse MapArea(TFI_Backend.Core.Model.Area area)
        {
            return new GetAreaQueryResponse
            {
                Id = area.Id,
                Nombre = area.Nombre,
                SubAreas = area.SubAreas?
                    .Select(sa => MapArea(sa))
                    .ToList() ?? new List<GetAreaQueryResponse>()
            };
        }
    }
}
