namespace TFI_Backend.DTO
{
    public class CreateReclamoDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int UsuarioId { get; set; }
        public int AreaId { get; set; }
        public List<IFormFile>? Imagenes { get; set; }
    }
}
