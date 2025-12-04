using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Area.GetAll
{
    public class GetAreaQueryResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public List<GetAreaQueryResponse> SubAreas { get; set; } = new();
    }
}
