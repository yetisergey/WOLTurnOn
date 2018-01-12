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
            BaseContext db = new BaseContext(int.Parse(User.Identity.Name));
            var temp = db.GetComputers().ToList();
            foreach (var xe in temp)
            {
                if (xe.Element("Id").Value == id.ToString())
                {
                    xe.RemoveAll();
                }
            }
            db.SaveChanges(temp);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Computer iop)
        {
            BaseContext db = new BaseContext(int.Parse(User.Identity.Name));
            db.AddComputer(iop);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(Guid id)
        {
            BaseContext db = new BaseContext(int.Parse(User.Identity.Name));
            var computers = db.GetComputers().ToList();
            foreach (var computer in computers)
            {
                if (computer.Element("Id").Value == id.ToString())
                {
                    return View(
                        new Computer() {
                            Id = Guid.Parse(computer.Element("Id").Value),
                            Name = computer.Element("Name").Value,
                            MacAddress = computer.Element("MacAddress").Value,
                            IpAddress = computer.Element("IpAddress").Value
                        });
                }
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Edit(Computer iop)
        {
            BaseContext db = new BaseContext(int.Parse(User.Identity.Name));
            var computers = db.GetComputers().ToList();
            foreach (var computer in computers)
            {
                if (computer.Element("Id").Value == iop.Id.ToString())
                {
                    computer.Element("Name").Value = iop.Name;
                    computer.Element("MacAddress").Value = iop.MacAddress == null ? "" : iop.MacAddress;
                    computer.Element("IpAddress").Value = iop.IpAddress == null ? "" : iop.IpAddress;
                }
            }
            db.SaveChanges(computers);
            return RedirectToAction("Index");
        }

        public ActionResult TurnOn(Guid id)
        {
            BaseContext db = new BaseContext(int.Parse(User.Identity.Name));

            var temp = db.GetComputers().ToList();
            foreach (var u in temp)
            {
                if (u.Element("Id").Value == id.ToString())
                {
                    Wake.Execute(u.Element("MacAddress").Value.Replace(":", "").Replace("-", ""));
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }

        public ActionResult Exit()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Enter");
        }
    }
}