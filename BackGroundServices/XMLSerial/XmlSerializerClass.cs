using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TcmbImplementationStudy.BackGroundServices.XMLSerial
{
    public class XmlSerializerClass
    {
        private readonly string Url = "https://www.tcmb.gov.tr/kurlar/{0}.xml";
        private readonly WebClient _webClient = new WebClient() { Encoding = Encoding.UTF8 };
        private readonly XmlSerializer _serializer = new XmlSerializer(typeof(Tarih_Date));

        public Task<Tarih_Date> GetTargetData(DateTime date)
        {
            var day = date.Day > 0 && date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var month = date.Month > 0 && date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var uri = new Uri(string.Format(Url, $"{date.Year}{month}/{day}{month}{date.Year}"));
            try
            {
                var data = _webClient.DownloadString(uri);
                var dataReader = new StringReader(data);
                var deserialize = (Tarih_Date) _serializer.Deserialize(dataReader);
                _webClient.Dispose();
                return Task.FromResult(deserialize);
            }
            catch (WebException)
            {
                Console.WriteLine("Indicative exchange rate information is not published" +
                                  " on public holidays, weekends and half working days.");
                Environment.Exit(-1);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("XML Document reader error");
                Environment.Exit(-1);
            }
            return null;
        }


    }
}
