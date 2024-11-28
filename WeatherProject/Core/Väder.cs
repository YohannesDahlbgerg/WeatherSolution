using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherProject.Core
{
    public class Väder
    {
        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public decimal Medeltemperatur { get; set; }  
        public decimal Medelluftfuktighet { get; set; }
        public decimal MögelRisk { get; set; }
        public string Vinter { get; set; }
        public string Höst { get; set; }

    }
}
