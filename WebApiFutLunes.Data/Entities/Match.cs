using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFutLunes.Data.Models;

namespace WebApiFutLunes.Data.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public string LocationTitle { get; set; }
        public string LocationMapUrl { get; set; }
        public DateTime MatchDate { get; set; }
        public int PlayerLimit { get; set; }
        public ICollection<ApplicationUser> Players { get; set; }
    }
}
