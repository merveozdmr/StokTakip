
using Rotativa;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Filtreler;

namespace StokTakip.Controllers
{
    [Authorize]
    [ActionFilter]
    public class FirmaController : _TemelController
    {
        Model1 ctx = new Model1();

        public ActionResult Index(string Search)
        {

            List<Firma> firmalar = ctx.Firma.ToList();
            // Eğer Search parametresi gerçekten bir string'i içeriyorsa, filmler sorgusu, Search parametresinin değeri ile filtreleme yapabilmesi
            if (!string.IsNullOrEmpty(Search))
            {

                firmalar = ctx.Firma.Where(a => a.FirmaAdi.Contains(Search) || a.YetkiliAdSoyad.Contains(Search) || a.YetkiliUnvani.Contains(Search) || a.Ulke.Contains(Search) || a.Sehir.Contains(Search)
                || a.Adres.Contains(Search) || a.Telefon.Contains(Search) || a.Faks.Contains(Search) || a.Email.Contains(Search) || a.WebSayfasi.Contains(Search))
                                    .ToList();

            }
            return View(firmalar);
        }

        [HttpPost]
        public string Index(FormCollection fc, string Search)
        {
            return Search;
        }

        public ActionResult FirmaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FirmaEkle(Firma frm)
        {
            if (ModelState.IsValid)
            {
                MailGonder("merveozdemir.9393@gmail.com", "Firma Ekleme", User.Identity.Name + " Kullanıcısı Yeni Bir Firma Ekledi.");
                ctx.Firma.Add(frm);
                ctx.SaveChanges();
                return RedirectToAction("Index", "Firma");
            }
            return View(frm);
        }

        public ActionResult FirmaSil(int id)
        {
            Firma f = ctx.Firma.FirstOrDefault(x => x.FirmaID == id);
            return View(f);
        }

        [HttpPost]
        public ActionResult FirmaSil(Firma f)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Firma Silme", User.Identity.Name + " Kullanıcısı Bir Firma Sildi.");
            Firma frm = ctx.Firma.FirstOrDefault(x => x.FirmaID == f.FirmaID);
            ctx.Firma.Remove(frm);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Firma");
        }

        public ActionResult FirmaGuncelle(int id)
        {
            Firma fir = (from a in ctx.Firma where a.FirmaID == id select a).FirstOrDefault();
            return View(fir);
        }

        [HttpPost]
        public ActionResult FirmaGuncelle(Firma fir)
        {
            if (ModelState.IsValid) {
                MailGonder("merveozdemir.9393@gmail.com", "Firma Güncelleme", User.Identity.Name + " Kullanıcısı Firma Bilgilerini Güncelledi.");
                Firma mevcut = (from a in ctx.Firma where a.FirmaID == fir.FirmaID select a).FirstOrDefault();
                mevcut.FirmaAdi = fir.FirmaAdi;
                mevcut.YetkiliAdSoyad = fir.YetkiliAdSoyad;
                mevcut.YetkiliUnvani = fir.YetkiliUnvani;
                mevcut.Ulke = fir.Ulke;
                mevcut.Sehir = fir.Sehir;
                mevcut.Adres = fir.Adres;
                mevcut.Telefon = fir.Telefon;
                mevcut.Email = fir.Email;
                mevcut.WebSayfasi = fir.WebSayfasi;
                ctx.SaveChanges();
                return RedirectToAction("Index", new { id = fir.FirmaID });
            }
            return View(fir);
        }

        public ActionResult FirmaPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/FirmaListesi.pdf")
            };
        }

    }
    
}