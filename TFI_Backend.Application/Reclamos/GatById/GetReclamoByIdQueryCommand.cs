using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Reclamos.GatById
{
    public class GetReclamoByIdQueryCommand
    {
        public int ReclamoId { get; set; }
        public int UsuarioId { get; set; }
    }
}
