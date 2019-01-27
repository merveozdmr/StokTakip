
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rotativa;
using StokTakip.Filtreler;

namespace StokTakip.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    [ActionFilter]
    public class PersonelController : _TemelController
    {
        Model1 ctx = new Model1();
 
        public ActionResult Index(string Search)
        {
            List<Personel> personeller = ctx.Personel.ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                personeller = ctx.Personel.Where(a => a.PersonelAd.Contains(Search)
                || a.PersonelSoyad.Contains(Search)
                || a.TcNo.Contains(Search)
                || a.Cinsiyet.Contains(Search)
                || a.DogumTarih.ToString().Contains(Search)
                || a.PersonelAdres.Contains(Search)
                || a.PersonelTelefon.Contains(Search)
                || a.KayitTarih.ToString().Contains(Search)
                || a.Email.Contains(Search))
                                                           .ToList();

            }
            return View(personeller);
        }

        [HttpPost]
        public string Index(FormCollection fc, string Search)
        {
            return Search;
        }

        public ActionResult PersonelEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel p)
        {
            if(ModelState.IsValid)
            {
                MailGonder("merveozdemir.9393@gmail.com", "Personel Ekleme", User.Identity.Name + " Kullanıcısı Yeni Bir Personel Ekledi.");
                ctx.Personel.Add(p);
                ctx.SaveChanges();
                return RedirectToAction("Index", "Personel");
            }
            return View(p);
            
        }
       
        public ActionResult PersonelSil(int id)
        {
            Personel p = ctx.Personel.FirstOrDefault(x => x.PersonelID == id);
            return View(p);
        }
       
        [HttpPost]
        public ActionResult PersonelSil(Personel p)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Personel Silme", User.Identity.Name + " Kullanıcısı Bir Personel Sildi.");
            Personel per = ctx.Personel.FirstOrDefault(x => x.PersonelID == p.PersonelID);
            ctx.Personel.Remove(per);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult PersonelGuncelle(int id)
        {
            Personel per = (from a in ctx.Personel where a.PersonelID == id select a).FirstOrDefault();
            return View(per);

        }
     
        [HttpPost]
        public ActionResult PersonelGuncelle(Personel per)
        {
            if (ModelState.IsValid)
            {
                MailGonder("merveozdemir.9393@gmail.com", "Personel Güncelleme", User.Identity.Name + " Kullanıcısı Personel Bilgilerini Güncelledi");
                Personel mevcut = (from a in ctx.Personel where a.PersonelID == per.PersonelID select a).FirstOrDefault();
                mevcut.PersonelAd = per.PersonelAd;
                mevcut.PersonelSoyad = per.PersonelSoyad;
                mevcut.TcNo = per.TcNo;
                mevcut.Cinsiyet = per.Cinsiyet;
                mevcut.DogumTarih = per.DogumTarih;
                mevcut.PersonelAdres = per.PersonelAdres;
                mevcut.PersonelTelefon = per.PersonelTelefon;
                mevcut.Email = per.Email;
                mevcut.KayitTarih = per.KayitTarih;
                //mevcut.Rol.RolAdi = per.Rol.RolAdi;
                ctx.SaveChanges();
                return RedirectToAction("Index", new { id = per.PersonelID });
            }
            return View(per);

        }

        public ActionResult PersonelPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/PersonelListesi.pdf")
            };
        }




    }
}