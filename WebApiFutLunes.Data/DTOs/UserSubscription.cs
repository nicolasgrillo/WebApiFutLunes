using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiFutLunes.Data.DTOs
{
    public class UserSubscription
    {
        public ApplicationUser User { get; set; }
        public DateTime SubscriptionDate { get; set; }
    }
}
