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
        [StringLength(20, ErrorMessage = "Nimi ei tohi olla pikem kui 20 tähemärki")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sisesta ürituse kuupäev")]
        [DataType(DataType.Date, ErrorMessage = "Sisesta kuupäev")]
        [CurrentDate(ErrorMessage = "Toimunud üritust ei saa lisada")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Sisesta ürituse toimumise koht")]
        [StringLength(50, ErrorMessage = "Kohanimi ei tohi olla pikem kui 50 tähemärki")]
        public string Place { get; set; }

        [StringLength(5000, ErrorMessage = "Liiga pikk tekst")]
        [Required(ErrorMessage = "Sisesta ürituse informatsioon")]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}