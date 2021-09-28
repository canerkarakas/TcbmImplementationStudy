using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TcmbImplementationStudy.EF;
using TcmbImplementationStudy.Models;

namespace TcmbImplementationStudy
{
    public class Service2
    {
        /*private static ILogger<Service2> _logger;

        public Service2(ILogger<Service2> logger)
        {
            _logger = logger;
        }*/

        public List<Currency> Get(string kod)
        {
            var currency = new Currency() { Kod = kod };
            AppDbContext appDbContext = new AppDbContext();
            var currencies = GetDailyChanges(currency, appDbContext);
            currencies.Sort();
            return currencies;
            //DailyInfo(currencies);

        }

        private List<Currency> GetDailyChanges(Currency currency, AppDbContext appDbContext)
        {
            List<Currency> currencies = new List<Currency>();
            foreach (var elementCurrency in appDbContext.Currencies)
            {
                if (elementCurrency.Kod.Equals(currency.Kod))
                {
                    currencies.Add(elementCurrency);
                }
            }
            return currencies;
        }

        private void DailyInfo(List<Currency> currencies)
        {
            foreach (var element in currencies)
            {
                Console.WriteLine($"TRY/{element.Kod} {element.Tarih} {element.ForexBuying}");
            }
        }
    }
}
