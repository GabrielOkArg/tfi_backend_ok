
using Microsoft.EntityFrameworkCore;
using TFI_Backend.Core.Model;
using TFI_Backend.Infrastructure.Data;


namespace TFI_Backend.Infrastructure.Entities
{
    public class DbInitializer
    {
        public static void Initialize(GestionReclamosDbContext context)
        {
            // Asegura que la DB existe
             context.Database.EnsureCreated();
            //context.Database.Migrate();

            // Si ya existen usuarios, no hace nada
            if (context.Usuarios.Any()) return;
            //if (context.Usuarios.Count() > 0) return;
            // Hash simple para pruebas (después usamos BCrypt)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword("1234");

            var admin = new User
            {
                Nombre = "Administrador",
                Email = "admin@gestion.com",
                PasswordHash = passwordHash,
                Rol = "Admin"
            };

            context.Usuarios.Add(admin);
            context.SaveChanges();
        }
    }
}
