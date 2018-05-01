using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Registreerimissüsteem.Models
{
    public class Company
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Sisesta nimi")]
        [StringLength(30, ErrorMessage = "Nimi ei tohi olla pikem kui 20 tähemärki")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sisesta registrinumber")]
        public long RegistryNumber { get; set; }

        [Required(ErrorMessage = "Sisesta osavõtjate arv")]
        [Range(0, 250, ErrorMessage = "Osavõtjate arv ei tohi olla suurem kui 250")]
        public int NumberOfParticipants { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Sisesta ettevõtte informatsioon")]
        [StringLength(5000, ErrorMessage = "Liiga pikk tekst")]
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