﻿using System.ComponentModel.DataAnnotations;

namespace WebApiFutLunes.Models.Player
{
    public class AddPlayerToMatchModel
    {
        [Required]
        public string UserName { get; set; }
    }
}