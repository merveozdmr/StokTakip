using Rotativa;
using StokTakip.App_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StokTakip.Filtreler;
using StokTakip.Models;

namespace StokTakip.Controllers
{
    [Authorize]
    [ActionFilter]
    public class KullaniciController : Controller
    {
        Model1 ctx = new Model1();
        public ActionResult Index()
        {
            MembershipUserCollection users = Membership.GetAllUsers();
            
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Ekle()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Ekle(Kullanici k )
        {

            MembershipCreateStatus durum;
            Membership.CreateUser(k.KullaniciAdi, k.Sifre, k.Email, k.GizliSoru, k.GizliCevap, true, out durum);
            string mesaj = "";
            switch (durum)//Girişde oluşabilecek hatalar
            {
                case MembershipCreateStatus.Success:
                    break;
                case MembershipCreateStatus.InvalidUserName:
                    mesaj += "Geçersiz Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    mesaj += "Geçersiz Şifre";
                    break;
                case MembershipCreateStatus.InvalidQuestion:
                    mesaj += "Geçersiz Gizli Soru";
                    break;
                case MembershipCreateStatus.InvalidAnswer:
                    mesaj += "Geçersiz Gizli Cevap";
                    break;
                case MembershipCreateStatus.InvalidEmail:
                    mesaj += "Geçersiz Email.";
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    mesaj += "Kullanılmış Kullanıcı Adı";
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    mesaj += "Kullanılmış Mail Adresi Girildi.";
                    break;
                case MembershipCreateStatus.UserRejected:
                    mesaj += "Kullanıcı Engel Hatası ";
                    break;
                case MembershipCreateStatus.InvalidProviderUserKey:
                    mesaj += "Geçersiz Kullanıcı Key Hatası.";
                    break;
                case MembershipCreateStatus.DuplicateProviderUserKey:
                    mesaj += "Kullanılmış Kullanıcı Key Hatası.";
                    break;
                case MembershipCreateStatus.ProviderError:
                    mesaj += "Üye Yönetimi Sağlayıcı Hatası. Şifre";
                    break;
                default:
                    break;
            }
            ViewBag.Mesaj = mesaj;
            if (durum == MembershipCreateStatus.Success)
                return RedirectToAction("Index");
            else
                return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult RolAta()
        {
            ViewBag.Roller = Roles.GetAllRoles().ToList();
            ViewBag.Kullanicilar = Membership.GetAllUsers();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult RolAta(string KullaniciAdi, string RolAdi)
        {
            Roles.AddUserToRole(KullaniciAdi, RolAdi);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public string UyeRolleri(string kullaniciAdi)
        {
            List<string> roller = Roles.GetRolesForUser(kullaniciAdi).ToList();
            string rol = "";
            foreach (string r in roller)
            {
                rol += r + "-";
            }
            rol = rol.Remove(rol.Length - 1, 1);
            return rol;
        }

        public ActionResult KullanıcıPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/KullanıcıListesi.pdf")
            };
        }
    }
}