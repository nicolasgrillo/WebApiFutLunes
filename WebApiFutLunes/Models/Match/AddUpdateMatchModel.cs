using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFutLunes.Models.Match
{
    public class AddUpdateMatchModel
    {
        [Required]
        [MaxLength(100)]
        public string LocationTitle { get; set; }
        public Uri LocationMapUrl { get; set; }
        public DateTime MatchDate { get; set; }
        public int PlayerLimit { get; set; }
    }
}