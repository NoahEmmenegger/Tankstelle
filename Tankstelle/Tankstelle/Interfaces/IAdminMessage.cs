using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;

namespace Tankstelle.Interfaces
{
    /// <summary>
    /// Interface für Hinweis Nachricht, welche dem Admin angezeigt wird
    /// </summary>
    public interface IAdminMessage
    {
        string Description { get; set; }
        MessageStatus Status { get; set; }
    }
}
