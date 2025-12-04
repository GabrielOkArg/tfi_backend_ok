

namespace TFI_Backend.Core.Model
{
    public class Area : EntityGeneral
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Autorreferencia (para el árbol)
        public int? ParentAreaId { get; set; }
        public Area? ParentArea { get; set; }
        public ICollection<Area> SubAreas { get; set; } = new List<Area>();
    }
}
