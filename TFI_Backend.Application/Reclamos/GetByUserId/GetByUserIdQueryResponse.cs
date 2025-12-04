

namespace TFI_Backend.Application.Reclamos.GetByUserId
{
    public class GetByUserIdQueryResponse
    {

        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Estado { get; set; } = "Pendiente"; // Pendiente / EnProceso / Resuelto
        public int UsuarioId { get; set; }
    }
}
