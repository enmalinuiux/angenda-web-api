using System;
using System.Collections.Generic;

namespace agenda_web_api.Models.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Business { get; set; }
        public DateTime Birth { get; set; }
        public byte UserType { get; set; }
        public string AddressStreet { get; set; }
        public string AddressCity { get; set; }
        public string AddressCountry { get; set; }
    }
}