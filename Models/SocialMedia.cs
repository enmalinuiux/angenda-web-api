using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace agenda_web_api.Models
{
    [Table("social_media")]
    public partial class SocialMedia
    {
        public SocialMedia()
        {
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

        [InverseProperty("Sm")]
        public virtual ICollection<UserSm> UserSm { get; set; }
    }
}
