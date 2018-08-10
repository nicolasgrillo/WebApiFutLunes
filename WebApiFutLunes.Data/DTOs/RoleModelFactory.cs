using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApiFutLunes.Data.DTOs
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;

        public RoleReturnModel Create(IdentityRole appRole)
        {
            return new RoleReturnModel
            {
                Url = _UrlHelper.Link("GetRoleById", new { id = appRole.Id}),
                Id = appRole.Id,
                Name = appRole.Name
            };
        }
    }

    public class RoleReturnModel
    {
        public string Url { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
