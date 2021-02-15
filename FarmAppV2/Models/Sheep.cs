using FarmAppV2.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppV2.Models
{
    public class Sheep
    {

        [Key]
        public int SheepId { get; set; }
        [Required]
        [Display(Name = "Κωδικός/Όνομα Ζώου")]
        public string SheepName { get; set; }
        [Display(Name = "Φύλο")]
        public Gender Gender { get; set; }
        [Display(Name = "Ημερομηνία Γέννησης")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Κωδικός/Όνομα Μητέρας")]
        public string MothersName { get; set; }
        [Display(Name = "Κωδικός/Όνομα Πατέρα")]
        public string FathersName { get; set; }
        public ICollection<SheepVaccine> SheepVaccines { get; set; }
        [Display(Name = "Αποβολή")]
        public bool Miscarriage { get; set; }

        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public IdentityUser Owner { get; set; }
    }
}
