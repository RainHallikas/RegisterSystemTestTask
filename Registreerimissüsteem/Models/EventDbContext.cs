using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Registreerimissüsteem.Models
{
    public class EventDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            modelBuilder.Entity<Event>().ToTable("Events");
            modelBuilder.Entity<Person>().ToTable("Persons");
            modelBuilder.Entity<Company>().ToTable("Companies");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }

        public List<Event> GetEvents() {
            EventDbContext db = new EventDbContext();
            return db.Events.ToList();
        }

        public List<Person> GetPersons() {
            EventDbContext db = new EventDbContext();
            return db.Persons.ToList();
        }

        public List<Company> GetCompanies() {
            EventDbContext db = new EventDbContext();
            return db.Companies.ToList();
        }

        public List<PaymentMethod> GetPaymentMethods() {
            EventDbContext db = new EventDbContext();
            return db.PaymentMethods.ToList();
        }

        public void AddPaymentMethods() {
            EventDbContext db = new EventDbContext();
            var bank = new PaymentMethod { Name = "Pangaülekanne" };
            var cash = new PaymentMethod { Name = "Sularaha" };
            db.PaymentMethods.Add(bank);
            db.PaymentMethods.Add(cash);
            db.SaveChanges();
        }
    }
}