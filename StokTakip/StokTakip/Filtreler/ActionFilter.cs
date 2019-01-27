using StokTakip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace StokTakip.Filtreler
{
    public class ActionFilter : FilterAttribute, IActionFilter
    {
        Model1 ctx = new Model1();
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

            ctx.Log.Add(new Log()
            {
                ActionName = filterContext.ActionDescriptor.ActionName,
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                Tarih = DateTime.Now,
                Bilgi = "OnActionExecuted"
            });
            ctx.SaveChanges();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ctx.Log.Add(new Log()
            //{
            //    ActionName = filterContext.ActionDescriptor.ActionName,
            //    ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //    Tarih = DateTime.Now,
            //    Bilgi = "OnActionExecuteing"

            //});
            //ctx.SaveChanges();
        }
    }
}