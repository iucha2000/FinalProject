using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLogicLayer.Enums
{
    public enum Operations
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4,
        DisplayAll = 5
    }
}
