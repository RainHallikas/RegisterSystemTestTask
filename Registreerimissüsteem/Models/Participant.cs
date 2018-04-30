using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Registreerimissüsteem.Models
{
    public class Participant
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string TypeName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Number { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public int ParticipantId { get; set; }
    }
}