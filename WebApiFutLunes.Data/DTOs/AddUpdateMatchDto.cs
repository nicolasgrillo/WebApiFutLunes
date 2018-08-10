using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFutLunes.Data.DTOs
{
    public class AddUpdateMatchDto
    {
        public string LocationTitle { get; set; }
        public string LocationMapUrl { get; set; }
        public DateTime MatchDate { get; set; }
        public int PlayerLimit { get; set; }
    }
}
