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
        public string Plats { get; set; }
        public decimal Temperatur { get; set; }
        public decimal Luftfuktighet { get; set; }

    }
}
