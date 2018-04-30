using Registreerimissüsteem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Registreerimissüsteem.Controllers
{
    public class HomeController : Controller
    {
        EditModel m = new EditModel();
        public ActionResult Index()
        {
            var db = new EventDbContext();
            var events = db.Events.ToList();
            var paymentMethods = db.PaymentMethods.ToList();
            if (paymentMethods.Count < 1) db.AddPaymentMethods();
            return View(events);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Participants(int id)
        {
            var db = new EventDbContext();
            m.PaymentMethods = db.GetPaymentMethods();
            var events = db.GetEvents();
            foreach (Event e in events)
            {
                if (e.Id == id)
                {
                    m.SelectedEventId = id;
                    m.Event = e;
                    break;
                }
            }
            m.Companies = db.GetCompanies();
            m.Persons = db.GetPersons();
            m.Participants = new List<Participant>();

            foreach (Company c in m.Companies) {
                if (c.Active == false || c.EventId != m.SelectedEventId) continue;
                Participant p = new Participant();
                p.TypeName = "Company";
                p.Name = c.Name;
                p.Number = c.RegistryNumber;
                p.RegisterDate = c.RegisterDate;
                p.ParticipantId = c.Id;
                m.Participants.Add(p);
            }

            foreach (Person n in m.Persons)
            {
                if (n.Active == false || n.EventId != m.SelectedEventId) continue;
                Participant p = new Participant();
                p.TypeName = "Person";
                p.Name = n.FirstName + " " + n.LastName;
                p.Number = n.PersonalCode;
                p.RegisterDate = n.RegisterDate;
                p.ParticipantId = n.Id;
                m.Participants.Add(p);
            }
            m.Participants = m.Participants.OrderByDescending(o => o.RegisterDate).ToList();
            return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCompany(EditModel m)
        {
            EventDbContext db = new EventDbContext();
            var company = db.GetCompanies().FirstOrDefault(x => x.EventId == m.SelectedEventId && x.Id == m.Company.Id);
            if (company == null) {
                var newCompany = new Company
                {
                    Name = m.Company.Name,
                    RegistryNumber = m.Company.RegistryNumber,
                    NumberOfParticipants = m.Company.NumberOfParticipants,
                    PaymentMethodId = m.SelectedPaymentMethodId,
                    Info = m.Company.Info,
                    EventId = m.SelectedEventId,
                    RegisterDate = DateTime.Now,
                    Active = true
                };
                db.Companies.Add(newCompany);
                db.SaveChanges();
            }
            else {
                var dbCompany = db.Companies.FirstOrDefault(x => x.EventId == m.SelectedEventId && x.Id == m.Company.Id);
                dbCompany.Name = m.Company.Name;
                dbCompany.RegistryNumber = m.Company.RegistryNumber;
                dbCompany.NumberOfParticipants = m.Company.NumberOfParticipants;
                dbCompany.PaymentMethodId = m.SelectedPaymentMethodId;
                dbCompany.Info = m.Company.Info;
                dbCompany.EventId = m.SelectedEventId;
                UpdateModel(dbCompany);
                db.SaveChanges();
            }
            return RedirectToAction("Participants", new { id = m.SelectedEventId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePerson(EditModel m)
        {
            EventDbContext db = new EventDbContext();
            var person = db.GetPersons().FirstOrDefault(x => x.EventId == m.SelectedEventId && x.Id == m.Person.Id);
            if (person == null)
            {
                var newPerson = new Person
                {
                    FirstName = m.Person.FirstName,
                    LastName = m.Person.LastName,
                    PersonalCode = m.Person.PersonalCode,
                    PaymentMethodId = m.SelectedPaymentMethodId,
                    Info = m.Person.Info,
                    EventId = m.SelectedEventId,
                    RegisterDate = DateTime.Now,
                    Active = true
                };
                db.Persons.Add(newPerson);
                db.SaveChanges();
            }
            else
            {
                var dbPerson = db.Persons.FirstOrDefault(x => x.EventId == m.SelectedEventId && x.Id == m.Person.Id);
                dbPerson.FirstName = m.Person.FirstName;
                dbPerson.LastName = m.Person.LastName;
                dbPerson.PersonalCode = m.Person.PersonalCode;
                dbPerson.PaymentMethodId = m.SelectedPaymentMethodId;
                dbPerson.Info = m.Person.Info;
                UpdateModel(dbPerson);
                db.SaveChanges();
            }
            return RedirectToAction("Participants", new { id = m.SelectedEventId });
        }

        public ActionResult PersonEditor(int id, int eventId)
        {
            var db = new EventDbContext();
            m.SelectedEventId = eventId;
            m.PaymentMethods = db.GetPaymentMethods();
            foreach (Person p in db.GetPersons())
            {
                if (p.Id == id)
                {
                    m.Person = p;
                    m.Person.PaymentMethod = db.PaymentMethods.FirstOrDefault(x => x.Id == m.Person.PaymentMethodId);
                    m.SelectedPaymentMethodId = m.Person.PaymentMethod.Id;
                    break;
                }
            }
            return View(m);
        }

        public ActionResult CompanyEditor(int id, int eventId)
        {
            var db = new EventDbContext();
            m.SelectedEventId = eventId;
            m.PaymentMethods = db.GetPaymentMethods();
            foreach (Company c in db.GetCompanies())
            {
                if (c.Id == id)
                {
                    m.Company = c;
                    m.Company.PaymentMethod = db.PaymentMethods.FirstOrDefault(x => x.Id == m.Company.PaymentMethodId);
                    m.SelectedPaymentMethodId = m.Company.PaymentMethod.Id;
                    break;
                }
            }
            return View(m);
        }

        public ActionResult DeactivatePerson(int id, int eventId)
        {
            EventDbContext db = new EventDbContext();
            var dbPerson = db.Persons.FirstOrDefault(x => x.Id == id && x.EventId == eventId);
            dbPerson.Active = false;
            UpdateModel(dbPerson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeactivateCompany(int id, int eventId)
        {
            EventDbContext db = new EventDbContext();
            var xd = db.Companies;
            var y = db.GetCompanies();
            var dbCompany = db.Companies.FirstOrDefault(x => x.Id == id && x.EventId == eventId);
            dbCompany.Active = false;
            UpdateModel(dbCompany);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeactivateEvent(int id)
        {
            EventDbContext db = new EventDbContext();
            var dbEvent = db.Events.FirstOrDefault(x => x.Id == id);
            dbEvent.Active = false;
            UpdateModel(dbEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}