using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Core.Model
{
    public class User : EntityGeneral
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; 
        public string Rol { get; set; } = "Usuario"; 
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
    }
}
