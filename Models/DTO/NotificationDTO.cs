using System;
using System.Collections.Generic;

namespace agenda_web_api.Models.DTO
{
    public class NotificationDTO
    {
        public string Id { get; set; }
        public byte Type { get; set; }
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public byte Status{ get; set; }
    }
}