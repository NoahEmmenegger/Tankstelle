using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    class AdminMessage : IAdminMessage
    {
        public string Description { get; set; }
        public MessageStatus Status { get; set; }
    }
}
