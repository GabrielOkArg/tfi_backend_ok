

namespace TFI_Backend.Application.Reclamos
{
    public class CreateReclamoCommand
    {

        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public int AreaId { get; set; }
        public List<string>? ImagenRutas { get; set; }

    }
}
