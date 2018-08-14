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
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public DateTime SubscriptionDate { get; set; }

        public UserSubscription(){}

        public UserSubscription(ApplicationUser user, DateTime date)
        {
            User = user.UserName;
            SubscriptionDate = date;
        }
    }
}
