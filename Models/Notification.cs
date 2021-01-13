using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda_web_api.Models
{
    [Table("notification")]
    public partial class Notification
    {
        [Key]
        [Column("id")]
        [StringLength(50)]
        public string Id { get; set; }
        [Required]
        [Column("contact_id")]
        [StringLength(50)]
        public string ContactId { get; set; }
        [Required]
        [Column("user_id")]
        [StringLength(50)]
        public string UserId { get; set; }
        [Required]
        [Column("message")]
        [StringLength(100)]
        public string Message { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime Date { get; set; }
        [Column("status")]
        public byte Status { get; set; }
        [Column("type")]
        public byte Type { get; set; }

        [ForeignKey(nameof(ContactId))]
        [InverseProperty("NotificationContact")]
        public virtual User Contact { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("NotificationUser")]
        public virtual User User { get; set; }
    }
}
