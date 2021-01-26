using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda_web_api.Models
{
    [Table("user_sm")]
    public partial class UserSm
    {
        [Key]
        [Column("user_id")]
        [StringLength(50)]
        public string UserId { get; set; }
        [Key]
        [Column("sm_id")]
        [StringLength(50)]
        public string SmId { get; set; }
        [Required]
        [Column("url")]
        [StringLength(256)]
        public string Url { get; set; }

        [ForeignKey(nameof(SmId))]
        [InverseProperty(nameof(SocialMedia.UserSm))]
        public virtual SocialMedia Sm { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserSm")]
        public virtual User User { get; set; }
    }
}
