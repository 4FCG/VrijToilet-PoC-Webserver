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
    public class MetingenController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Metingen
        public ViewResult Index(string sortOrder, string SearchWord, string MetingFilter, int? page, string currentFilter)
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

            var metingen = from s in db.Metings.Include(m => m.Sensor) select s;

            int maxMeting = metingen.Max(m => m.meting_count);
            ViewBag.Max = maxMeting;

            if (!String.IsNullOrEmpty(SearchWord))
            {
                metingen = metingen.Where(s => s.Sensor.name.Contains(SearchWord));
            }

            if (!String.IsNullOrEmpty(MetingFilter))
            {
                int[] range = MetingFilter.Split(new string[] { " - " }, StringSplitOptions.None).Select(s => int.Parse(s)).ToArray();
                //used these variables due to linq arrayindex error
                int min = range[0];
                int max = range[1];
                metingen = metingen.Where(m => m.meting_count >= min && m.meting_count <= max);
                ViewBag.ChosenMin = min;
                ViewBag.ChosenMax = max;
            }
            else
            {
                ViewBag.ChosenMin = 0;
                ViewBag.ChosenMax = maxMeting;
            }

            metingen = metingen.OrderBy(s => s.Sensor.name);

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            return View(metingen.ToPagedList(pageNumber, pageSize));
        }

        // GET: Metingen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meting meting = db.Metings.Find(id);
            if (meting == null)
            {
                return HttpNotFound();
            }
            return View(meting);
        }

        // GET: Metingen/Create
        public ActionResult Create()
        {
            ViewBag.sensor_id = new SelectList(db.Sensors, "sensor_id", "name");
            return View();
        }

        // POST: Metingen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "meting_id,received_timestamp,meting_count,sensor_id")] Meting meting)
        {
            if (ModelState.IsValid)
            {
                db.Metings.Add(meting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.sensor_id = new SelectList(db.Sensors, "sensor_id", "name", meting.sensor_id);
            return View(meting);
        }

        // GET: Metingen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meting meting = db.Metings.Find(id);
            if (meting == null)
            {
                return HttpNotFound();
            }
            ViewBag.sensor_id = new SelectList(db.Sensors, "sensor_id", "name", meting.sensor_id);
            return View(meting);
        }

        // POST: Metingen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "meting_id,received_timestamp,meting_count,sensor_id")] Meting meting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.sensor_id = new SelectList(db.Sensors, "sensor_id", "name", meting.sensor_id);
            return View(meting);
        }

        // GET: Metingen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meting meting = db.Metings.Find(id);
            if (meting == null)
            {
                return HttpNotFound();
            }
            return View(meting);
        }

        // POST: Metingen/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Meting meting = db.Metings.Find(id);
            db.Metings.Remove(meting);
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
