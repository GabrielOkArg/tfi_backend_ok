using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Core.Interfaces
{
    public interface IReclamoRepository
    {

        void Add(Reclamo reclamo);
        Task AddAsync(Reclamo reclamo);
        Task SaveChangesAsync();
        Reclamo? GetById(int id);
        IEnumerable<Reclamo> GetAll();
        IEnumerable<Reclamo> GetByUsuarioId(int usuarioId);
        void Update(Reclamo reclamo);
        void Delete(Reclamo reclamo);
        void SaveChanges();
       
    }
}
