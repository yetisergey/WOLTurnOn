namespace WebApplication.Controllers
{
    using System.Web.Http;
    using System.Web.Security;
    using Models.Dto;
    using Models;
    using System;
    using Models.Utils;

    [AllowAnonymous]
    public class EnterAPIController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody]User user)
        {
            try
            {
                BaseContext bc = new BaseContext(user.Login.Trim(), HashPassword.ConvertToMd5HashGUID(user.Password.Trim()));
                FormsAuthentication.SetAuthCookie(bc.UserId.ToString(), false);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}