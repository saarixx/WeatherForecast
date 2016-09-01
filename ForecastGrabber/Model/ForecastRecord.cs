using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastGrabber.Model
{
    public class ForecastRecord
    {
        public string City { get; set; }
        public DateTime Date { get; set; }
        public int DayTemperature { get; set; }
        public int NightTemperature { get; set; }
    }
}
