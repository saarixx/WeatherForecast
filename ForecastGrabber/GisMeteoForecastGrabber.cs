using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForecastGrabber.Model;
using System.Net.Http;
using System.Net;
using HtmlAgilityPack;

namespace ForecastGrabber
{
    public class GisMeteoForecastGrabber : IForecastGrabber
    {
        private static readonly Dictionary<string, string> cityCodes = new Dictionary<string, string>()
        {
            ["Dnipro"] = "dnipro-5077"
        };

        public async Task<ForecastRecord[]> RetrieveForecastAsync(string city)
        {
            string cityCode = cityCodes[city];
            HttpClient http = new HttpClient();
            var response = await http.GetByteArrayAsync("https://www.gismeteo.ua/weather-" + cityCode + "/14-days/");
            string source = Encoding.UTF8.GetString(response);
            source = WebUtility.HtmlDecode(source);
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(source);
            HtmlNodeCollection nodes = document.DocumentNode.SelectNodes("//table/td/div[@class='s_date']");
            List<ForecastRecord> result = new List<ForecastRecord>();
            foreach (HtmlNode dateNode in nodes)
            {
                DateTime date;
                DateTime.TryParseExact(dateNode.InnerText, "d.MM", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
                HtmlNodeCollection tempNodes = dateNode.SelectNodes("../..//span[@class='value m_temp c']");
                result.Add(new ForecastRecord
                {
                    City = city,
                    Date = date,
                    NightTemperature = ParseTemp(tempNodes[0]),
                    DayTemperature = ParseTemp(tempNodes[1])
                });
            }
            return result.ToArray();
        }

        private static int ParseTemp(HtmlNode node)
        {
            string text = node.InnerText;
            if (text.StartsWith("+"))
            {
                text = text.Substring(1);
            }
            int result;
            int.TryParse(text, out result);
            return result;
        }
    }
}
