using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CRUDEntityProject.Models;

namespace CRUDEntityProject.Controllers
{
    public class HomeController : Controller
    {
        private EmployDBModelEntities db = new EmployDBModelEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.Employs.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employ employ = db.Employs.Find(id);
            if (employ == null)
            {
                return HttpNotFound();
            }
            return View(employ);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Employ_Id,First_Name,Last_Name,DOB,City")] Employ employ)
        {
            if (ModelState.IsValid)
            {
                var isIdExist = (from m in db.Employs
                                 where m.Employ_Id == employ.Employ_Id
                                 select m).FirstOrDefault();
                if(isIdExist != null)
                {
                    ModelState.AddModelError("", "Employ ID Sould't be duplicate");
                    return View(employ);
                }
                db.Employs.Add(employ);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employ);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employ employ = db.Employs.Find(id);
            if (employ == null)
            {
                return HttpNotFound();
            }
            return View(employ);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Employ_Id,First_Name,Last_Name,DOB,City")] Employ employ)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employ).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employ);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employ employ = db.Employs.Find(id);
            if (employ == null)
            {
                return HttpNotFound();
            }
            return View(employ);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employ employ = db.Employs.Find(id);
            db.Employs.Remove(employ);
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
