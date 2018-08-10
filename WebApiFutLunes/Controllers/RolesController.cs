using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

namespace WebApiFutLunes.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/Roles")]
    public class RolesController : ApiController
    {
        private ApplicationRoleManager _AppRoleManager;

        protected ApplicationRoleManager AppRoleManager
        {
            get
            {
                return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set { _AppRoleManager = value; }
        }

    }
}
