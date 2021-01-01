using System;
using System.Collections.Generic;

namespace agenda_web_api.Models
{
    public partial class SocialMedia
    {
        public SocialMedia()
        {
            UserSm = new HashSet<UserSm>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserSm> UserSm { get; set; }
    }
}
