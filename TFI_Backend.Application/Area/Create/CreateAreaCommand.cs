using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Area.Create
{
    public class CreateAreaCommand
    {

        public string Nombre { get; set; }
        public int ParentAreaId { get; set; }
    }
}
