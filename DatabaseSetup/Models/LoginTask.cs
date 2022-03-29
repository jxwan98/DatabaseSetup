using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Models
{
    public class LoginTask
    {
        public int day { get; set; }
        public Reward reward { get; set; }
        public uint timestamp { get; set; }
    }
}
