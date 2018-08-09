using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiFutLunes.Models.Player
{
    public class PlayerModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Appearances { get; set; }
    }
}