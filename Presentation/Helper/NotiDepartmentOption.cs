using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Helper
{
    internal class NotiDepartmentOption
    {
        public int DepartmentId;
        public string DepartmentName;

        public override string ToString()
        {
            return DepartmentName;
        }
    }
}
