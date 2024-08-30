using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domine
{
    public enum Role
    {
        Client = 0,
        Employee = 1,
        Admin = 2,
    }

    public enum OrderState
    {
        Send = 0,
        Complete = 1,
    }
}
