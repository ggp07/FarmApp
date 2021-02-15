using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FarmAppV2.Models
{
    public class SheepVaccine
    {
        public int SheepId { get; set; }
        public Sheep Sheep { get; set; }

        public int VaccineId { get; set; }
        public Vaccine Vaccine { get; set; }
    }
}
