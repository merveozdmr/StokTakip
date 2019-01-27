using Rotativa;
using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StokTakip.Filtreler;
using System.Web.Security;

namespace StokTakip.Controllers
{
    [Authorize]    
    [ActionFilter]
    public class UrunController : _TemelController
    {
        Model1 ctx = new Model1();
        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult Index(string Search)
        {
            ViewBag.KategoriListesi = new SelectList(ctx.Kategori, "KategoriID", "KategoriAdi");
            List<Urun> urunler = ctx.Urun.ToList();
            
            if (!string.IsNullOrEmpty(Search))
            {
                urunler = ctx.Urun.Where(a => a.UrunAdi.Contains(Search)
                || a.Marka.Contains(Search)
                || a.Model.Contains(Search)
                || a.SeriNo.ToString().Contains(Search)
                || a.KayitYapan.Contains(Search)
                || a.AltKategori.AltKategoriAdi.Contains(Search)
                || a.UrunAlimTarihi.ToString().Contains(Search)
                || a.Firma.FirmaAdi.Contains(Search)
                || a.Fiyat.ToString().Contains(Search)
                || a.GarantiBitisTarihi.ToString().Contains(Search)
                || a.LisansBitisTarihi.ToString().Contains(Search))
                                                           .ToList();
            }
            return View(urunler);
        }

        [HttpPost]
        public string Index(FormCollection fc, string Search)
        {
            return Search;
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunEkle()
        {
            ViewBag.Firma = ctx.Firma.ToList();
            ViewBag.KategoriListesi = new SelectList(ctx.Kategori, "KategoriID", "KategoriAdi");
            ViewBag.AltKategori = ctx.AltKategori.ToList();
            ViewBag.Kategori = ctx.Kategori.ToList();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunEkle(Urun u)
        {
           
                MailGonder("merveozdemir.9393@gmail.com", "Ürün Ekleme", User.Identity.Name + " Kullanıcısı Yeni Bir Ürün ekledi");
                u.KayitYapan = User.Identity.Name;
                ViewBag.Firma = ctx.Firma.ToList();
                ctx.Urun.Add(u);
                ctx.SaveChanges();
                ViewBag.KategoriListesi = new SelectList(ctx.Kategori, "KategoriID", "KategoriAdi");
                return RedirectToAction("Index", "Urun");
           
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunSil(int id)
        {
            ViewBag.KategoriListesi = new SelectList(ctx.Kategori, "KategoriID", "KategoriAdi");
            Urun u = ctx.Urun.FirstOrDefault(x => x.UrunID == id);
            return View(u);
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunSil(Urun u)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Ürün Silme", User.Identity.Name + " Kullanıcısı Yeni Bir Personel Sildi.");
            Urun urn = ctx.Urun.FirstOrDefault(x => x.UrunID == u.UrunID);
            ctx.Urun.Remove(urn);
            ctx.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunGuncelle(int id)
        {
            ViewBag.Firma = ctx.Firma.ToList();
            Urun urun = (from a in ctx.Urun where a.UrunID == id select a).FirstOrDefault();
            ViewBag.KategoriListesi = new SelectList(ctx.Kategori, "KategoriID", "KategoriAdi");
            return View(urun);
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunGuncelle(Urun urun)
        {           
                MailGonder("merveozdemir.9393@gmail.com", "Ürün Güncelleme", User.Identity.Name + " Kullanıcısı Ürün Bilgilerini Güncelledi.");
                Urun mevcut = (from a in ctx.Urun where a.UrunID == urun.UrunID select a).FirstOrDefault();
                ViewBag.Firma = ctx.Firma.ToList();
                mevcut.UrunAdi = urun.UrunAdi;
                mevcut.Marka = urun.Marka;
                mevcut.Model = urun.Model;
                mevcut.SeriNo = urun.SeriNo;
                mevcut.Fiyat = urun.Fiyat;
                mevcut.FirmaID = urun.FirmaID;
                mevcut.KayitYapan = urun.KayitYapan;
                //mevcut.AltKategori.KategoriID = urun.AltKategori.KategoriID;
                mevcut.AltKategoriID = urun.AltKategoriID;
                ViewBag.KategoriListesi = new SelectList(ctx.Kategori, "KategoriID", "KategoriAdi");
                mevcut.UrunAlimTarihi = urun.UrunAlimTarihi;
                mevcut.GarantiBitisTarihi = urun.GarantiBitisTarihi;
                mevcut.GarantiUyarıTarihi = urun.GarantiUyarıTarihi;
                mevcut.LisansBitisTarihi = urun.LisansBitisTarihi;
                ctx.SaveChanges();
                return RedirectToAction("Index", new { id = urun.UrunID });

        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/UrunListesi.pdf")
            };
        }
        //................................ÜRÜN STOK..............................................
        [Authorize(Roles = "Admin,Depo Sorumlusu,Kullanıcı")]
        public ActionResult UrunStok(string Search)
        {
            List<Stok> stok = ctx.Stok.ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                stok = ctx.Stok.Where(a => a.Urun.UrunAdi.Contains(Search)
                || a.StokAdet.ToString().Contains(Search)
                || a.KritikStokAdedi.ToString().Contains(Search)
                || a.StokKayitYapan.Contains(Search)
                || a.Raf.RafAdi.Contains(Search)
                || a.Birim.BirimAdi.Contains(Search)
                )
                                                           .ToList();          
            }
            return View(stok);
        }

        [HttpPost]
        public string UrunStok(FormCollection fc, string Search)
        {
            return Search;
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunStokEkle()
        {
            ViewBag.DepoListesi = new SelectList(ctx.Depo, "DepoID", "DepoAdi");
            //ViewBag.Raf = ctx.Raf.ToList();
            ViewBag.Urun = ctx.Urun.ToList();
            ViewBag.Birim = ctx.Birim.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunStokEkle(Stok s)
        {
            if (ModelState.IsValid)
            {
                s.StokKayitYapan = User.Identity.Name;
                MailGonder("merveozdemir.9393@gmail.com", "Ürün Stok Ekleme", User.Identity.Name + " Kullanıcısı Yeni Bir Stok ekledi");
                ViewBag.DepoListesi = new SelectList(ctx.Depo, "DepoID", "DepoAdi");
                ViewBag.Raf = ctx.Raf.ToList();
                ViewBag.Birim = ctx.Birim.ToList();
                ctx.Stok.Add(s);
                ctx.SaveChanges();
                return RedirectToAction("UrunStok", "Urun");
            }
            return View();
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunStokSil(int id)
        {
            ViewBag.DepoRaf = new SelectList(ctx.Depo, "DepoID", "DepoAdi");
            Stok s = ctx.Stok.FirstOrDefault(x => x.StokID == id);
            return View(s);
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunStokSil(Stok s)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Ürün Stok Silme", User.Identity.Name + " Kullanıcısı Bir Stok Sildi.");
            Stok stk = ctx.Stok.FirstOrDefault(x => x.StokID == s.StokID);
            ctx.Stok.Remove(stk);
            ctx.SaveChanges();
            return RedirectToAction("UrunStok");
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunStokAzalt()
        {
            //ViewBag.SilinmisUrunler = ctx.SilinmisUrunler.ToList();
            ViewBag.Stok = ctx.Stok.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunStokAzalt(SilinmisUrunler s)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Stok Azaltma", User.Identity.Name + " Kullanıcısı Stok Azalttı.");
            //ViewBag.SilinmisUrunler = ctx.SilinmisUrunler.ToList();
            ViewBag.Stok = ctx.Stok.ToList();
            ctx.SilinmisUrunler.Add(s);
            ctx.SaveChanges();
            return RedirectToAction("UrunStok", "Urun");
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunStokArtir()
        {
            //ViewBag.SilinmisUrunler = ctx.SilinmisUrunler.ToList();
            ViewBag.Stok = ctx.Stok.ToList();
            return View();
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunStokArtir(AlinanUrunler s)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Stok Artırma", User.Identity.Name + " Kullanıcısı Stok Arttırdı.");
            //ViewBag.SilinmisUrunler = ctx.SilinmisUrunler.ToList();
            ViewBag.Stok = ctx.Stok.ToList();
            
            ctx.AlinanUrunler.Add(s);
            ctx.SaveChanges();
            return RedirectToAction("UrunStok", "Urun");
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        public ActionResult UrunStokGuncelle(int id)
        {          
            ViewBag.DepoListesi = new SelectList(ctx.Depo, "DepoID", "DepoAdi");
            ViewBag.Raf = ctx.Raf.ToList();
            ViewBag.Urun = ctx.Urun.ToList();
            ViewBag.Birim = ctx.Birim.ToList();
            Stok urun = (from a in ctx.Stok where a.StokID == id select a).FirstOrDefault();
            return View(urun);
        }

        [Authorize(Roles = "Admin,Depo Sorumlusu")]
        [HttpPost]
        public ActionResult UrunStokGuncelle(Stok urun)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Stok Güncelleme", User.Identity.Name + " Kullanıcısı Stok Güncelledi.");
            Stok mevcut = (from a in ctx.Stok where a.StokID == urun.StokID select a).FirstOrDefault();
            ViewBag.Urun = ctx.Urun.ToList();
            mevcut.UrunID = urun.UrunID;
            //mevcut.Urun.Marka = urun.Urun.Marka;
            //mevcut.Urun.Model = urun.Urun.Model;
            mevcut.StokAdet = urun.StokAdet;
            mevcut.BirimID = urun.BirimID;
            mevcut.RafID = urun.RafID;
            mevcut.StokKayitYapan = urun.StokKayitYapan; 
            ViewBag.DepoListesi = new SelectList(ctx.Depo, "DepoID", "DepoAdi");
            ViewBag.Raf = ctx.Raf.ToList();
            ctx.SaveChanges();
            return RedirectToAction("Index", new { id = urun.StokID });
        }

        public ActionResult UrunStokPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/UrunStokListesi.pdf")
            };
        }
       


        //...............................ÜRÜN DURUM....................................................
        public ActionResult UrunDurum(string Search)
        {
            List<UrunDurum> hareket = ctx.UrunDurum.ToList();
            if (!string.IsNullOrEmpty(Search))
            {
                hareket = ctx.UrunDurum.Where(a => a.Urun.UrunAdi.Contains(Search)
                || a.UrunDurumu.Contains(Search)
                || a.HareketTarihi.ToString().Contains(Search)
                || a.Aciklama.Contains(Search)
                )
                                                           .ToList();

            }
            return View(hareket);
        }

        [HttpPost]
        public string UrunDurum(FormCollection fc, string Search)
        {
            return Search;
        }

        public ActionResult UrunDurumEkle()
        {
            ViewBag.Urun = ctx.Urun.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult UrunDurumEkle(UrunDurum h)
        {           
                MailGonder("merveozdemir.9393@gmail.com", "Ürün Durum Ekleme", User.Identity.Name + " Kullanıcısı Ürün Hareketi Ekledi.");
                ctx.UrunDurum.Add(h);
                ctx.SaveChanges();
                return RedirectToAction("UrunDurum", "Urun");

        }

        public ActionResult UrunDurumSil(int id)
        {
            UrunDurum u = ctx.UrunDurum.FirstOrDefault(x => x.DurumID == id);
            return View(u);
        }

        [HttpPost]
        public ActionResult UrunDurumSil(UrunDurum u)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Ürün Durum Silme", User.Identity.Name + " Kullanıcısı Ürün Hareketi Silme.");
            UrunDurum ud = ctx.UrunDurum.FirstOrDefault(x => x.DurumID == u.DurumID);
            ctx.UrunDurum.Remove(ud);
            ctx.SaveChanges();
            return RedirectToAction("UrunDurum");
        }

        public ActionResult UrunDurumGuncelle(int id)
        {
            UrunDurum ud = (from a in ctx.UrunDurum where a.DurumID == id select a).FirstOrDefault();
            ViewBag.Urun = ctx.Urun.ToList();
            return View(ud);
        }

        [HttpPost]
        public ActionResult UrunDurumGuncelle(UrunDurum ud)
        {
            MailGonder("merveozdemir.9393@gmail.com", "Ürün Durum Güncelleme", User.Identity.Name + " Kullanıcısı Ürün Hareketi Güncelledi.");
            UrunDurum mevcut = (from a in ctx.UrunDurum where a.DurumID == ud.DurumID select a).FirstOrDefault();
            mevcut.UrunID = ud.UrunID;
            mevcut.UrunDurumu = ud.UrunDurumu;
            mevcut.Aciklama = ud.Aciklama;
            mevcut.HareketTarihi = ud.HareketTarihi;
            ViewBag.Urun = ctx.Urun.ToList();
            ctx.SaveChanges();
            return RedirectToAction("UrunDurum", new { id = ud.DurumID });
        }

        [HttpGet]
        public ActionResult AltKategoriGetir(int KategoriID)
        {

            var altkategoriler = ctx.AltKategori
                .Where(h => h.KategoriID == KategoriID)
                .Select(h => new { AltKategoriID = h.AltKategoriID, AltKategoriAdi = h.AltKategoriAdi })
                .ToList();
            return Json(altkategoriler, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RafGetir(int DepoID)
        {
            var raflar = ctx.Raf
                .Where(h => h.DepoID == DepoID)
                .Select(h => new { RafID = h.RafID, RafAdi = h.RafAdi })
                .ToList();
            return Json(raflar, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UrunDurumPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/UrunDurumListesi.pdf")
            };
        }
    }
}