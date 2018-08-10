using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFutLunes.Data.Entities
{
    public class NLogEntry
    {
        public int Id { get; set; }
        public string Exception { get; set; }
        public string Level { get; set; }
        public string StackTrace { get; set; }
        public string Username { get; set; }
        public string Date { get; set; }
    }
}
