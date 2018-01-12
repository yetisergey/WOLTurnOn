namespace WebApplication.Controllers
{
    using System.Web.Http;
    using System.Web.Security;
    using Models.Dto;

    [AllowAnonymous]
    public class EnterAPIController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post([FromBody]User user)
        {
            if (user.Login.Trim() == "*" && user.Password.Trim() == "*")
            {
                FormsAuthentication.SetAuthCookie(user.Login, false);
                return Ok();
            }
            else
            {
                return BadRequest("Не правильный логин или пароль!");
            }
        }
    }
}