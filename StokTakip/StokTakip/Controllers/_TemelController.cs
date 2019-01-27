using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace StokTakip.Controllers
{
    public class _TemelController : Controller
    {
        public bool MailGonder(string EPosta, string Konu, String Icerik)
        {
            try
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.gmail.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("merveozdemir.9393@gmail.com", "fygjgjf");

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("merveozdemir.9393@gmail.com", "Stok Takip");


                mail.To.Add(EPosta);
                mail.Subject = Konu;
                mail.IsBodyHtml = true;
                mail.Body = Icerik;
                sc.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
