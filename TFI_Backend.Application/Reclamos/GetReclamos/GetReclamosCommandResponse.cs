using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Reclamos.GetReclamos
{
    public class GetReclamosCommandResponse
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaFin { get; set; }
        public string ComentarioTecnico { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public decimal Costo { get; set; } = 0;
        public int AreaId { get; set; }
        public string? AreaNombre { get; set; }

        public List<string> Imagenes { get; set; } = new();
        public string  Presupuesto { get; set; }
    }
}
