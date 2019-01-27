using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StokTakip.Filtreler;

namespace StokTakip.Controllers
{
    [Authorize]
    [ActionFilter]
    public class RolController : Controller
    {
        public ActionResult Index()
        {
            List<string> roller = Roles.GetAllRoles().ToList();
            return View(roller);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Ekle()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Ekle(string RolAdi)
        {
            Roles.CreateRole(RolAdi);
            return RedirectToAction("Index");
        }

        public ActionResult RolPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/RolListesi.pdf")
            };
        }
    }
}