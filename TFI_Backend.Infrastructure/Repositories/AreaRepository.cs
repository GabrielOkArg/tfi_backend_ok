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
    public class AreaRepository : IAreaRepository
    {

        private readonly GestionReclamosDbContext _context;

        public AreaRepository(GestionReclamosDbContext context)
        {
            _context = context;
        }
        public void Add(Area area)
        {
            _context.Areas.Add(area);
        }

        public async Task AddAsync(Area area)
        {
            await _context.Areas.AddAsync(area);
            
        }

        public void Delete(Area area)
        {
           _context.Areas.Remove(area);
        }

        public IEnumerable<Area> GetAll()
        {
            return _context.Areas
        .Include(a => a.SubAreas)
        .ToList();
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }

        public void Update(Area area)
        {
           _context.Areas.Update(area);
        }
    }
}
