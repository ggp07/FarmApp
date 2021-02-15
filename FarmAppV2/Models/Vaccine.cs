using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppV2.Models
{
    public class Vaccine
    {
        [Key]
        public int VaccineId { get; set; }
        public string Name { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime VaccineDate { get; set; }
        public ICollection<SheepVaccine> SheepVaccines { get; set; }
        public string OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public IdentityUser Owner { get; set; }
    }
}
