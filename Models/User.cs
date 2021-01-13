using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda_web_api.Models
{
    [Table("user")]
    public partial class User
    {
        public User()
        {
            InverseBusinessNavigation = new HashSet<User>();
            NotificationContact = new HashSet<Notification>();
            NotificationUser = new HashSet<Notification>();
            UserContactContact = new HashSet<UserContact>();
            UserContactUser = new HashSet<UserContact>();
            UserPhone = new HashSet<UserPhone>();
            UserSm = new HashSet<UserSm>();
        }

        [Key]
        [Column("id")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("last_name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [Column("pass")]
        [StringLength(100)]
        public string Pass { get; set; }
        [Column("business")]
        [StringLength(50)]
        public string Business { get; set; }
        [Column("birth", TypeName = "date")]
        public DateTime Birth { get; set; }
        [Column("user_type")]
        public byte UserType { get; set; }
        [Required]
        [Column("address_street")]
        [StringLength(50)]
        public string AddressStreet { get; set; }
        [Required]
        [Column("address_city")]
        [StringLength(50)]
        public string AddressCity { get; set; }
        [Required]
        [Column("address_country")]
        [StringLength(50)]
        public string AddressCountry { get; set; }

        [ForeignKey(nameof(Business))]
        [InverseProperty(nameof(User.InverseBusinessNavigation))]
        public virtual User BusinessNavigation { get; set; }
        [InverseProperty(nameof(User.BusinessNavigation))]
        public virtual ICollection<User> InverseBusinessNavigation { get; set; }
        [InverseProperty(nameof(Notification.Contact))]
        public virtual ICollection<Notification> NotificationContact { get; set; }
        [InverseProperty(nameof(Notification.User))]
        public virtual ICollection<Notification> NotificationUser { get; set; }
        [InverseProperty(nameof(UserContact.Contact))]
        public virtual ICollection<UserContact> UserContactContact { get; set; }
        [InverseProperty(nameof(UserContact.User))]
        public virtual ICollection<UserContact> UserContactUser { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserPhone> UserPhone { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserSm> UserSm { get; set; }
    }
}
