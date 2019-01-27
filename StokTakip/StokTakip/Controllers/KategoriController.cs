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
    public class KategoriController : Controller
    {
        Model1 ctx = new Model1();

        //
        // GET: /Kategori/
        public ActionResult Kategori(string Search)
        {
            List<Kategori> kategoriler = ctx.Kategori.ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                kategoriler = ctx.Kategori.Where(a => a.KategoriAdi.Contains(Search)
               )
                                                           .ToList();

            }
            return View(kategoriler);
        }

        [HttpPost]
        public string Kategori(FormCollection fc, string Search)
        {
            return Search;
        }

        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            if (ModelState.IsValid)
            {
                ctx.Kategori.Add(k);
                ctx.SaveChanges();
                return RedirectToAction("Kategori", "Kategori");
            }
            return View();
        }

        public ActionResult KategoriSil(int id)
        {
            Kategori k = ctx.Kategori.FirstOrDefault(x => x.KategoriID == id);
            return View(k);
        }

        [HttpPost]
        public ActionResult KategoriSil(Kategori k)
        {
            Kategori ktg = ctx.Kategori.FirstOrDefault(x => x.KategoriID == k.KategoriID);
            ctx.Kategori.Remove(ktg);
            ctx.SaveChanges();
            return RedirectToAction("Kategori","Kategori");
        }

        public ActionResult KategoriGuncelle(int id)
        {
            Kategori ktg = (from a in ctx.Kategori where a.KategoriID == id select a).FirstOrDefault();
            return View(ktg);

        }

        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori ktg)
        {
            Kategori mevcut = (from a in ctx.Kategori where a.KategoriID == ktg.KategoriID select a).FirstOrDefault();
            mevcut.KategoriAdi = ktg.KategoriAdi;
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = ktg.KategoriID });
        }

        public ActionResult KategoriPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/KategoriListesi.pdf")
            };
        }

        public ActionResult AltKategori(string Search)
        {
            List<AltKategori> altkategoriler = ctx.AltKategori.ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                altkategoriler = ctx.AltKategori.Where(a => a.AltKategoriAdi.Contains(Search)
               )
                                                           .ToList();

            }
            return View(altkategoriler);
        }

        [HttpPost]
        public string AltKategori(FormCollection fc, string Search)
        {
            return Search;
        }

        public ActionResult AltKategoriEkle()
        {
            ViewBag.Kategori = ctx.Kategori.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AltKategoriEkle(AltKategori ak)
        {          
                ctx.AltKategori.Add(ak);
                ctx.SaveChanges();
                return RedirectToAction("AltKategori", "Kategori");
        }
        public ActionResult AltKategoriSil(int id)
        {
            AltKategori ak = ctx.AltKategori.FirstOrDefault(x => x.AltKategoriID == id);
            return View(ak);
        }

        [HttpPost]
        public ActionResult AltKategoriSil(AltKategori ak)
        {
            AltKategori aktg = ctx.AltKategori.FirstOrDefault(x => x.AltKategoriID == ak.AltKategoriID);
            ctx.AltKategori.Remove(aktg);
            ctx.SaveChanges();
            return RedirectToAction("AltKategori");
        }

        public ActionResult AltKategoriGuncelle(int id)
        {
            AltKategori ktg = (from a in ctx.AltKategori where a.AltKategoriID == id select a).FirstOrDefault();
            return View(ktg);

        }

        [HttpPost]
        public ActionResult AltKategoriGuncelle(AltKategori ktg)
        {
            AltKategori mevcut = (from a in ctx.AltKategori where a.AltKategoriID == ktg.AltKategoriID select a).FirstOrDefault();
            mevcut.AltKategoriAdi = ktg.AltKategoriAdi;
            ctx.SaveChanges();
            return RedirectToAction("AltKategori", new { id = ktg.KategoriID });
        }

        public ActionResult KategoriListesi(string Search)
        {
            List<AltKategori> ktg = ctx.AltKategori.ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                ktg = ctx.AltKategori.Where(a => a.AltKategoriAdi.Contains(Search)
                || a.Kategori.KategoriAdi.Contains(Search)
                )
                                                           .ToList();

            }
            return View(ktg);
        }

        [HttpPost]
        public string KategoriListesi(FormCollection fc, string Search)
        {
            return Search;
        }

        public ActionResult AltKategoriPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/AltKategoriListesi.pdf")
            };
        }

        public ActionResult KategoriListesiPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/KategorilerListesi.pdf")
            };
        }

    }
}