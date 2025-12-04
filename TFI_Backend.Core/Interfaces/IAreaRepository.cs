using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Core.Interfaces
{
    public interface IAreaRepository
    {

        void Add(Area area);
        Task AddAsync(Area area);
        Task SaveChangesAsync();
        IEnumerable<Area> GetAll();
        void Update(Area area);
        void Delete(Area area);
    }
}
