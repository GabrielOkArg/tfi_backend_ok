using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Core.Model
{
    public class ReclamoPresupuesto
    {
        public int Id { get; set; }
        public string Ruta { get; set; } = string.Empty;

        public int ReclamoId { get; set; }
        public Reclamo Reclamo { get; set; } = null!;
    }
}
