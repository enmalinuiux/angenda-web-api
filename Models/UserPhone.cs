using System;
using System.Collections.Generic;

namespace agenda_web_api.Models
{
    public partial class UserPhone
    {
        public string Phone { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
