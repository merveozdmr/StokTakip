using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Filtreler;

namespace StokTakip.Controllers
{
    using App_Classes;
    using System.Web.Security;
    [Authorize]
    [ActionFilter]
    public class UyeController : Controller
    {
        [AllowAnonymous]
        public ActionResult GirisYap()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult GirisYap(Kullanici k, string Hatirla)
        {
            bool sonuc = Membership.ValidateUser(k.KullaniciAdi, k.Sifre);
            if (sonuc)
            {
                if (Hatirla == "on")
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, true);
                else
                    FormsAuthentication.RedirectFromLoginPage(k.KullaniciAdi, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mesaj = "Kullanıcı Adı veya Şifre Hatalı!";
                return View();
            }
        }

        public ActionResult CıkısYap()
        {
            FormsAuthentication.SignOut();
            return Redirect("GirisYap");
        }

        [AllowAnonymous]
        public ActionResult SifremiUnuttum()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult SifremiUnuttum(Kullanici k)
        {
            MembershipUser mu = Membership.GetUser(k.KullaniciAdi);
            if (mu.PasswordQuestion == k.GizliSoru)
            {
                string pwd = mu.ResetPassword(k.GizliCevap);
                mu.ChangePassword(pwd, k.Sifre);
                return RedirectToAction("GirisYap");
            }
            else
            {
                ViewBag.Mesaj = "Girilen Bilgiler Yanlıştı!";
                return View();
            }
        }
    }
}