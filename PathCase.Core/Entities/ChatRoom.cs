using System;
using System.Collections.Generic;

namespace PathCase.Core.Entities
{
    public class ChatRoom : BaseEntity
    {
        public string Name { get; set; }
        public IList<ChatLog> ChatLogs { get; set; }
    }

    public class ChatLog
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public ChatLog()
        {
            Date = DateTime.UtcNow;
        }
    }
}