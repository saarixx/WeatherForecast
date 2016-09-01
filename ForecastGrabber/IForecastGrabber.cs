using ForecastGrabber.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastGrabber
{
    public interface IForecastGrabber
    {
        Task<ForecastRecord[]> RetrieveForecastAsync(string city);
    }
}
