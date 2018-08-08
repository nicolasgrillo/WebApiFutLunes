using System;

namespace WebApiFutLunes.Models.Match
{
    public class AddMatchModel
    {
        public string LocationTitle { get; set; }
        public string LocationMapUrl { get; set; }
        public DateTime MatchDate { get; set; }
        public int PlayerLimit { get; set; }
    }
}