using Registreerimissüsteem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registreerimissüsteem.Controllers
{
    public class AddEventController : Controller
    {
        EditModel m = new EditModel();
        public ActionResult Index()
        {
            var db = new EventDbContext();
            var events = db.Events.ToList();
            m.Events = events;
            m.Events = new List<Event>();
            m.Events.AddRange(db.GetEvents());
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateEvent(EditModel m)
        {
            if (!ModelState.IsValid) return View("Index", m);
            var e = new Event()
            {
                Name = m.Event.Name,
                Date = m.Event.Date,
                Place = m.Event.Place,
                Info = m.Event.Info,
                Active = true

            };
            SaveEvent(e);
            return RedirectToAction("Index");
        }
        public Event SaveEvent(Event e)
        {
            EventDbContext db = new EventDbContext();
            var list = db.Events.ToList();
            if (list.Contains(list.Find(x => x.Name == e.Name))) return e;
            db.Events.Add(e);
            db.SaveChanges();
            return e;
        }
    }
}