using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Core.Interfaces
{
    public interface IUserContext
    {
        int? GetUserId();
        string? GetEmail();
        string? GetRole();
    }
}
