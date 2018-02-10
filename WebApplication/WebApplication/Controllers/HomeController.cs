namespace WebApplication.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using Models;
    using Utils;
    using Models.Dto;

    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Enter()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            db.DeleteComputer(id);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Computer computer)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            db.AddComputer(computer);
            return RedirectToAction("Index");
        }
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            db.AddUser(user);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(Guid id)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            var computer = db.GetComputer(id);
            if (computer != null)
            {
                return View(
                    new Computer()
                    {
                        Id = Guid.Parse(computer.Element("Id").Value),
                        Name = computer.Element("Name").Value,
                        MacAddress = computer.Element("MacAddress").Value,
                        IpAddress = computer.Element("IpAddress").Value
                    });
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(Computer computer)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            db.EditComputer(computer.Id, computer.Name, computer.IpAddress, computer.MacAddress);
            return RedirectToAction("Index");
        }

        public ActionResult TurnOn(Guid id)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            var computer = db.GetComputer(id);
            if (computer != null)
            {
                Options.Wake(computer.Element("MacAddress").Value.Replace(":", "").Replace("-", ""));
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult TurnOff(Guid id)
        {
            BaseContext db = new BaseContext(Guid.Parse(User.Identity.Name));
            var computer = db.GetComputer(id);
            if (computer != null)
            {
                Options.Wake(computer.Element("MacAddress").Value.Replace(":", "").Replace("-", ""));
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Enter");
        }
    }
}