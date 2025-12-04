using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Reclamos.Update
{
    public class UpdateReclamoCommandResponse
    {

        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
    }
}
