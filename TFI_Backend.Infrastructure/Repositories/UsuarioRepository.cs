using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Interfaces;
using TFI_Backend.Core.Model;
using TFI_Backend.Infrastructure.Data;

namespace TFI_Backend.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {

        private readonly GestionReclamosDbContext _context;

        public UsuarioRepository(GestionReclamosDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Usuarios.Add(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Usuarios.ToList();
        }

        public User? GetByEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
