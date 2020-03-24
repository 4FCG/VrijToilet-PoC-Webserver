using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Webserver_PoC.Models;
using X.PagedList;

namespace Webserver_PoC.Controllers
{
    public class SensorsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Sensors
        public ViewResult Index(string sortOrder, string SearchWord, int? page, string currentFilter) 
        {
            ViewBag.CurrentSort = sortOrder;

            if (SearchWord != null)
            {
                page = 1;
            }
            else
            {
                SearchWord = currentFilter;
            }

            ViewBag.CurrentFilter = SearchWord;

            var sensors = from s in db.Sensors select s;

            if (!String.IsNullOrEmpty(SearchWord))
            {
                sensors = sensors.Where(s => s.name.Contains(SearchWord));
            }

            sensors = sensors.OrderBy(s => s.name);

            int pageSize = 4;
            int pageNumber = (page ?? 1);


            return View(sensors.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sensors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // GET: Sensors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sensors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "sensor_id,name,location_description, longitude, latitude")] Sensor sensor)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Sensors.Add(sensor);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }

            return View(sensor);
        }

        // GET: Sensors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "sensor_id,name,location_description, longitude, latitude")] Sensor sensor)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sensor).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(sensor);
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
            
        }

        // GET: Sensors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sensor sensor = db.Sensors.Find(id);
            if (sensor == null)
            {
                return HttpNotFound();
            }
            return View(sensor);
        }

        // POST: Sensors/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                Sensor sensor = db.Sensors.Find(id);
                db.Sensors.Remove(sensor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
            
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
