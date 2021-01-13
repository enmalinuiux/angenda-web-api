using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda_web_api.Models
{
    [Table("user_contact")]
    public partial class UserContact
    {
        [Key]
        [Column("contact_id")]
        [StringLength(50)]
        public string ContactId { get; set; }
        [Key]
        [Column("user_id")]
        [StringLength(50)]
        public string UserId { get; set; }
        [Column("scheduled_date", TypeName = "date")]
        public DateTime ScheduledDate { get; set; }
        [Column("nickname")]
        [StringLength(50)]
        public string Nickname { get; set; }
        [Column("is_blocked")]
        public byte IsBlocked { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("UserContactContact")]
        public virtual User Contact { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserContactUser")]
        public virtual User User { get; set; }
    }
}
