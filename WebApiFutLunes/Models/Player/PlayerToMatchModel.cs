using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiFutLunes.Models.Player
{
    public class PlayerToMatchModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int MatchId { get; set; }
        public DateTime InscriptionDate { get; set; }
    }
}