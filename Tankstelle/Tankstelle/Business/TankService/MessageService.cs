using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tankstelle.Data;

namespace Tankstelle.Business.TankService
{
    class MessageService
    {
        #region private Felder
        private static MessageService uniqueInstance = null;
        private List<Message> messages = new List<Message>();
        #endregion

        #region Konstruktor
        private MessageService()
        {

        }
        #endregion

        public static MessageService CreateInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new MessageService();;
            }
            return uniqueInstance;
        }
        public void AddMessage(string title, string description)
        {
            Message message = new Message();
            message.Title = title;
            message.Description = description;

            messages.Add(message);
        }

        public List<Message> GetMessages()
        {
            return messages;
        }
    }
}
