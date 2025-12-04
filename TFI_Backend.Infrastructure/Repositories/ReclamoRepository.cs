using Microsoft.EntityFrameworkCore;
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
    public class ReclamoRepository : IReclamoRepository
    {

        private readonly GestionReclamosDbContext _context;

        public ReclamoRepository(GestionReclamosDbContext context)
        {
            _context = context;
        }
        public void Add(Reclamo reclamo)
        {
            _context.Reclamos.Add(reclamo);
        }

        public Reclamo? GetById(int id)
        {
            return _context.Reclamos.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Reclamo> GetAll()
        {
            return _context.Reclamos
                .Where(r => !r.isDelete)
                .Include(r => r.Usuario)
                .Include(r => r.Area)
                .Include(r => r.Imagenes)
                .Include(r => r.Presupuesto)
                .ToList();
        }

        public IEnumerable<Reclamo> GetByUsuarioId(int usuarioId)
        {
            return _context.Reclamos.Where(r => r.UsuarioId == usuarioId & r.isDelete == false).ToList();
        }

        public void Update(Reclamo reclamo)
        {
            _context.Reclamos.Update(reclamo);
        }

        public void Delete(Reclamo reclamo)
        {
            _context.Reclamos.Remove(reclamo);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task AddAsync(Reclamo reclamo)
        {
            await _context.Reclamos.AddAsync(reclamo);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    
    }
}
