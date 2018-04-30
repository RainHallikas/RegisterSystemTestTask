using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registreerimissüsteem.Models
{
    public class EditModel
    {
        public PaymentMethod PaymentMethod { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; }

        public Event Event { get; set; }
        public List<Event> Events { get; set; }

        public Person Person { get; set; }
        public List<Person> Persons { get; set; }

        public Company Company { get; set; }
        public List<Company> Companies { get; set; }

        public Participant Participant { get; set; }
        public List<Participant> Participants { get; set; }

        public int SelectedEventId { get; set; }
        public int SelectedPersonId { get; set; }
        public int SelectedCompanyId { get; set; }
        public int SelectedPaymentMethodId { get; set; }
        public bool SelectedParticipant { get; set; }
        public int SelectedParticipantId { get; set; }
    }
}