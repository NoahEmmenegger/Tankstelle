using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Enums;
using Tankstelle.Interfaces;

namespace Tankstelle.Business
{
    /// <summary>
    /// Hinweis Nachricht, welche dem Admin angezeigt wird
    /// </summary>
    class AdminMessage : IAdminMessage
    {
        /// <summary>
        /// Beschreibung der Nachricht
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Der Status der Nachricht
        /// </summary>
        public MessageStatus Status { get; set; }
    }
}
