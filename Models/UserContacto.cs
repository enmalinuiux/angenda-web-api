using System;
using System.Collections.Generic;

namespace agenda_web_api.Models
{
    public partial class UserContacto
    {
        public string ContactId { get; set; }
        public string UserId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Nickname { get; set; }
        public byte IsBlocked { get; set; }

        public virtual User Contact { get; set; }
        public virtual User User { get; set; }
    }
}
