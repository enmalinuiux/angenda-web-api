﻿using System;
using System.Collections.Generic;

namespace agenda_web_api.Models
{
    public partial class User
    {
        public User()
        {
            InverseBusinessNavigation = new HashSet<User>();
            UserContactoContact = new HashSet<UserContacto>();
            UserContactoUser = new HashSet<UserContacto>();
            UserPhone = new HashSet<UserPhone>();
            UserSm = new HashSet<UserSm>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public string Business { get; set; }
        public DateTime Birth { get; set; }
        public byte UserType { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }

        public virtual User BusinessNavigation { get; set; }
        public virtual ICollection<User> InverseBusinessNavigation { get; set; }
        public virtual ICollection<UserContacto> UserContactoContact { get; set; }
        public virtual ICollection<UserContacto> UserContactoUser { get; set; }
        public virtual ICollection<UserPhone> UserPhone { get; set; }
        public virtual ICollection<UserSm> UserSm { get; set; }
    }
}
