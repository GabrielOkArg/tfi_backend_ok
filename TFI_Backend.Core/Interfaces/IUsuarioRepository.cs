using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFI_Backend.Core.Model;

namespace TFI_Backend.Core.Interfaces
{
    public interface IUsuarioRepository
    {

        User? GetByEmail(string email);
        void Add(User user);
        IEnumerable<User> GetAll();

        void SaveChanges();
    }
}
