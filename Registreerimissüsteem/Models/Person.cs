using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Registreerimissüsteem.Models
{
    public class Person
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "Eesnimi ei tohi olla pikem kui 30 tähemärki")]
        [Required(ErrorMessage = "Sisesta eesnimi")]
        public string FirstName { get; set; }
        [StringLength(30, ErrorMessage = "Perenimi ei tohi olla pikem kui 30 tähemärki")]
        [Required(ErrorMessage = "Sisesta perenimi")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Sisesta isikukood")]
        public long PersonalCode { get; set; }
        public int PaymentMethodId { get; set; }
        [Required(ErrorMessage = "Sisesta isiku informatsioon")]
        [StringLength(1500, ErrorMessage = "Liiga pikk tekst")]
        public string Info { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public bool Active { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}