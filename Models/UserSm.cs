using System;
using System.Collections.Generic;

namespace agenda_web_api.Models
{
    public partial class UserSm
    {
        public string UserId { get; set; }
        public string SmId { get; set; }
        public string Url { get; set; }

        public virtual SocialMedia Sm { get; set; }
        public virtual User User { get; set; }
    }
}
