namespace TFI_Backend.DTO
{
    public class UpdateReclamoDto
    {
        public int Id { get; set; }           // ID del reclamo a modificar
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public string Reclamo { get; set; }
        public decimal? Costo { get; set; }
        public string Estado { get; set; }

        // Archivo tipo Word, Excel, PDF, TXT, etc.
        public IFormFile? PresupuestoArchivo { get; set; }
    }

}
