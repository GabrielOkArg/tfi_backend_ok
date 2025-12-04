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
    public class ReclamoPresupuesto : IreclamoPresupuesto
    {
        private readonly GestionReclamosDbContext _context;

        public ReclamoPresupuesto(GestionReclamosDbContext context)
        {
            _context = context;
        }
        public IEnumerable<TFI_Backend.Core.Model.ReclamoPresupuesto> GetAll()
        {
           return _context.ReclamoPresupuesto.ToList();
        }

       
    }
}
