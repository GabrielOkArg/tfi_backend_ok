using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFI_Backend.Application.Area.Update
{
    public class UpdateAreaCommandResponse
    {

        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsOk { get; set; }
    }
}
