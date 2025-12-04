using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Core.Model
{
    public class Reclamo: EntityGeneral
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente"; // Pendiente / EnProceso / Resuelto
        public string? Resolucion { get; set; }
        // Relación con Usuario
        public int UsuarioId { get; set; }
        public User Usuario { get; set; } = null!;
        public int AreaId { get; set; }
        public Area Area { get; set; } = null!;
        public decimal Costo { get; set; }

        public List<ReclamoImagen> Imagenes { get; set; } = new();
        public ReclamoPresupuesto Presupuesto { get; set; }

    }
}
