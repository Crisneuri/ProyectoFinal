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
    public class EmpleadosController : Controller
    {
        private FinalmenteEntities db = new FinalmenteEntities();

        // GET: Empleados
        public ActionResult Index(string Departa, string searchString, DateTime? mes = null)
        {
            var Depart = new List<string>();
        
            var Depart2 = from d in db.Departamentos
                          orderby d.Codigo_Departamento
                          select d.Codigo_Departamento;

            Depart.AddRange(Depart2.Distinct());
            ViewBag.Depart = new SelectList(Depart);


            var lista = from m in db.Empleados
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                lista = lista.Where(s => s.Nombre.Contains(searchString));
               
            }

            var empleados = from m in db.Empleados
                            select m;

            if (mes.HasValue)
            {
                empleados = empleados.Where(s => s.Fecha_Ingreso.Value.Day == mes.Value.Day && s.Fecha_Ingreso.Value.Month == mes.Value.Month && s.Fecha_Ingreso.Value.Year == mes.Value.Year);
            }
            return View(lista);
        }
      
        public ActionResult Activos(string Activos) {
            var Activo = from m in db.Empleados
                           select m;
            Activo = Activo.Where(s => s.Estatus == "Activo");
            return View(Activo);
        }
        public ActionResult Inactivos(string Inactivos)
        {
            var Inactivo = from m in db.Empleados
                           select m;
            Inactivo = Inactivo.Where(s => s.Estatus == "Inactivo");
            return View(Inactivo);
        }
        public ActionResult Lista() {

            return View(db.Empleados.ToList());
        }
        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Codigo,Nombre,Apellido,Telefono,Departamento,Cargo,Fecha_Ingreso,Salario,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Codigo,Nombre,Apellido,Telefono,Departamento,Cargo,Fecha_Ingreso,Salario,Estatus")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
