using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;

namespace Tankstelle.Interfaces
{
    public interface IAdminMessage
    {
        string Description { get; set; }
        MessageStatus Status { get; set; }
    }
}
