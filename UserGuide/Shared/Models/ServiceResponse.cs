using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGuide.Shared.Models
{
    public class ServiceResponse
    {
        public int Success { get; set; } = 200;
        public string Message { get; set; } = string.Empty;
    }
}
