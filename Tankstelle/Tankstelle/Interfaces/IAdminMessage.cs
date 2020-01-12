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
        /// <summary>
        /// Beschreibung der Nachricht
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Der Status der Nachricht
        /// </summary>
        MessageStatus Status { get; set; }
    }
}
