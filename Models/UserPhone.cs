using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda_web_api.Models
{
    [Table("user_phone")]
    public partial class UserPhone
    {
        [Key]
        [Column("phone")]
        [StringLength(15)]
        public string Phone { get; set; }
        [Required]
        [Column("user_id")]
        [StringLength(50)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserPhone")]
        public virtual User User { get; set; }
    }
}
