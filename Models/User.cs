using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace agenda_web_api.Models
{
    public partial class User
    {
        public User()
        {
            InverseBusinessNavigation = new HashSet<User>();
            UserContactContact = new HashSet<UserContact>();
            UserContactUser = new HashSet<UserContact>();
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

        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual User BusinessNavigation { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual ICollection<User> InverseBusinessNavigation { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember]
        public virtual ICollection<UserContact> UserContactContact { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual ICollection<UserContact> UserContactUser { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual ICollection<UserPhone> UserPhone { get; set; }
        [JsonIgnore] 
        [IgnoreDataMember] 
        public virtual ICollection<UserSm> UserSm { get; set; }
    }
}
