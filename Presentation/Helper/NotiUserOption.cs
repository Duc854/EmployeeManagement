using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helper
{
    internal class NotiUserOption
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;

        public override string ToString()
        {
            return UserName; 
        }
    }
}
