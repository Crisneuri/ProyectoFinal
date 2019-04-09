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
    public class Salidas_EmpleadosController : Controller
    {
        private FinalmenteEntities db = new FinalmenteEntities();

        // GET: Salidas_Empleados
        public ActionResult Index()
        {
           
            return View(db.Salidas_Empleados.ToList());
        }
        public ActionResult Salidas(DateTime? SearchString = null)
        {

            var empleados = from m in db.Salidas_Empleados
                            select m;

            if (SearchString.HasValue)
            {
                empleados = empleados.Where(s => s.Fecha_Salida.Value.Month == SearchString.Value.Month && s.Fecha_Salida.Value.Day == SearchString.Value.Day && s.Fecha_Salida.Value.Year == SearchString.Value.Year);
            }
            return View(empleados);
        }



        // GET: Salidas_Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas_Empleados salidas_Empleados = db.Salidas_Empleados.Find(id);
            if (salidas_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(salidas_Empleados);
        }

        // GET: Salidas_Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salidas_Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Empleado,Salida,Motivo,Fecha_Salida")] Salidas_Empleados salidas_Empleados)
        {
            var x = salidas_Empleados.Empleado;
            var q = (from a in db.Empleados where a.Codigo == x select a).First();
            q.Estatus = "Inactivo";
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                db.Salidas_Empleados.Add(salidas_Empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Empleado = new SelectList(db.Empleados, "ID", "Codigo", salidas_Empleados.Empleado);
            return View(salidas_Empleados);
        }

        // GET: Salidas_Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas_Empleados salidas_Empleados = db.Salidas_Empleados.Find(id);
            if (salidas_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(salidas_Empleados);
        }

        // POST: Salidas_Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Salida,Motivo,Fecha_Salida,Empleado")] Salidas_Empleados salidas_Empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salidas_Empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salidas_Empleados);
        }

        // GET: Salidas_Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salidas_Empleados salidas_Empleados = db.Salidas_Empleados.Find(id);
            if (salidas_Empleados == null)
            {
                return HttpNotFound();
            }
            return View(salidas_Empleados);
        }

        // POST: Salidas_Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salidas_Empleados salidas_Empleados = db.Salidas_Empleados.Find(id);
            db.Salidas_Empleados.Remove(salidas_Empleados);
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
