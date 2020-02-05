using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NorthwindSolution.AuthServerEntities
{
    public class Audience
    {
        [Key]
        [MaxLength(32)]
        public string ClientID { get; set; }

        [MaxLength(80)]
        [Required]
        public string Base64Secret { get; set; }

        [MaxLength(100)]
        public string ClientName { get; set; }
    }
}