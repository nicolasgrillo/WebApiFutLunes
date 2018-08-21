using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiFutLunes.Data.DTOs;

namespace WebApiFutLunes.Data.Entities
{
    public class Match
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LocationTitle { get; set; }
        public string LocationMapUrl { get; set; }
        public DateTime MatchDate { get; set; }
        public int PlayerLimit { get; set; }
        public bool Open { get; set; }
        public virtual ICollection<UserSubscription> Players { get; set; }
    }
}
