using System;
using System.Collections.Generic;

namespace agenda_web_api.Models.DTO
{
    public class ContactDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Nickname { get; set; }
        public byte IsBlocked { get; set; }
        public string Email { get; set; }
        public string Business { get; set; }
        public DateTime Birth { get; set; }
        public byte UserType { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
        public List<string> Phones { get; set; }    
    }
}