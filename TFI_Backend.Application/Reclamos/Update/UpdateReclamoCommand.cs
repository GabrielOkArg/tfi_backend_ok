using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Reclamos.Update
{
    public class UpdateReclamoCommand
    {
        public int Id { get; set; }           // ID del reclamo a modificar
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public string Reclamo { get; set; }
        public decimal? Costo { get; set; }
        public string Estado { get; set; }
        public string? PresupuestoRuta { get; set; }
    }
}
