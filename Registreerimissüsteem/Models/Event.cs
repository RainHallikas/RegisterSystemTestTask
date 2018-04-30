using Registreerimissüsteem.Aids;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registreerimissüsteem.Models
{
    public class Event
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Sisesta ürituse nimi")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sisesta ürituse kuupäev")]
        [DataType(DataType.Date, ErrorMessage = "Sisesta kuupäev")]
        [CurrentDate(ErrorMessage = "Toimunud üritust ei saa lisada")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Sisesta ürituse toimumise koht")]
        public string Place { get; set; }
        [StringLength(5000, ErrorMessage = "Liiga pikk tekst")]
        [Required(ErrorMessage = "Sisesta ürituse informatsioon")]
        public string Info { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}