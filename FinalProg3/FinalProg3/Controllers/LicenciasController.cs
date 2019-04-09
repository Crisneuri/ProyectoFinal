using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProg3.Model;

namespace FinalProg3.Controllers
{
    public class LicenciasController : Controller
    {
        private FinalmenteEntities db = new FinalmenteEntities();

        // GET: Licencias
        public ActionResult Index()
        {
            return View(db.Licencia.ToList());
        }

        // GET: Licencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }
            return View(licencia);
        }

        // GET: Licencias/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Licencias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Desde,Hasta,Motivo,Comentarios")] Licencia licencia)
        {
            if (ModelState.IsValid)
            {
                db.Licencia.Add(licencia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(licencia);
        }

        // GET: Licencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }
            return View(licencia);
        }

        // POST: Licencias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Desde,Hasta,Motivo,Comentarios")] Licencia licencia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(licencia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(licencia);
        }

        // GET: Licencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Licencia licencia = db.Licencia.Find(id);
            if (licencia == null)
            {
                return HttpNotFound();
            }
            return View(licencia);
        }

        // POST: Licencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Licencia licencia = db.Licencia.Find(id);
            db.Licencia.Remove(licencia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
