using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TcmbImplementationStudy.EF;
using TcmbImplementationStudy.Models;

namespace TcmbImplementationStudy.BackGroundServices.XMLSerial
{
    #region

    public class TcmbService : IHostedService, IDisposable
    {
        private static ILogger<TcmbService> _logger;
        private Timer _timer;
        private Thread _timerThread;
        private int _counterTimer;
        private bool _flag = true;
        private static Tarih_Date _tarihDate;
        private static AppDbContext _appDbContext = new AppDbContext();

        public TcmbService(ILogger<TcmbService> logger)
        {
            _logger = logger;
            _counterTimer = 0;
        }

        public void Dispose()
        {
            switch (_timerThread.IsAlive)
            {
                case true:
                    try
                    {
                        _timerThread.Join();
                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation($"Thread Join Fail {e}");
                        throw;
                    }
                    break;
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tcmb Service running.");
            _timerThread = new Thread(DoThreadWork);
            _timerThread.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tcmb Service is stopping.");
            _flag = false;
            return Task.CompletedTask;
        }

        private void DoThreadWork()
        {
            while (_flag)
            {
                var date = DateTime.Now;
                _logger.LogInformation($"Tcmb Service Thread is working. Now Date : {date}");
                if (date.Hour >= 9 && date.Hour < 21)
                {
                    _timer = new Timer(callback: DoTimerWork, state: _counterTimer, dueTime: TimeSpan.Zero,
                        period: TimeSpan.FromSeconds(2));
                    /*_timer = new Timer(callback: new TimerCallback(DoWork2), state: _counterTimer, dueTime: TimeSpan.Zero,
                        period: TimeSpan.FromHours(1));*/
                    while (_counterTimer < 10 && _flag)
                    {
                        Task.Delay(1000).Wait();
                    }
                    /*var tempDateTime = new DateTime(date.Year, date.Month, date.Day, hour: date.Hour, date.Minute+1,
                        date.Second + 15, date.Millisecond);*/
                    var tempDateTime = DateTime.Now;
                    /*var tempDateTime = new DateTime(date.Year, date.Month, date.Day+1, hour: 9, 0,
                        0);*/
                    try
                    {
                        _timer.Dispose();
                        _counterTimer = 0;
                        _logger.LogInformation($"{DateTime.Now:HH:mm:ss tt zz} : done.");
                        if (_flag)
                        {
                            Thread.Sleep((int)(tempDateTime - date).TotalMilliseconds);
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation($"{e}");
                        throw;
                    }
                }
                else
                {
                    switch (_flag)
                    {
                        case true when date.Hour >= 18:
                        {
                            /*var tempDateTime = new DateTime(date.Year, date.Month, date.Day + 1, 9, 0,
                                0, 0);*/
                            var tempDateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute,
                                date.Second+3);
                            try
                            {
                                Thread.Sleep((int)(tempDateTime - date).TotalMilliseconds);
                            }
                            catch (Exception e)
                            {
                                _logger.LogInformation($"{e}");
                                throw;
                            }
                        }
                            break;
                        case true:
                        {
                            /*var tempDateTime = new DateTime(date.Year, date.Month, date.Day, 9, 0,
                                0, 0);*/
                            var tempDateTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute,
                                date.Second+3);
                            try
                            {
                                Thread.Sleep((int)(tempDateTime - date).TotalMilliseconds);
                            }
                            catch (Exception e)
                            {
                                _logger.LogInformation($"{e}");
                                throw;
                            }
                        }
                            break;
                    }
                }
            }
        }

        private void DoTimerWork(object state)
        {
            switch (_flag)
            {
                case true:
                    Interlocked.Increment(ref _counterTimer);
                    _logger.LogInformation("Running Timer.");
                    _logger.LogInformation($"counter timer = {_counterTimer}");
                    var xmlSerializer = new XmlSerializerClass();
                    _tarihDate = xmlSerializer.GetTargetData(new DateTime(year:2021, month:9, day:24)).Result;
                    GetResultAllCurrencies(true);
                    /*var size = _appDbContext.Currencies.Count();
                    _logger.LogInformation($"size : {size}");
                    foreach (var currency in _appDbContext.Currencies)
                    {
                        HardDeleteAppDbContext(currency);
                    }

                    _appDbContext.SaveChanges();*/
                    break;
            }
        }

        public static Tarih_Date GetTarihDate(){return _tarihDate;}

        private static void GetResultAllCurrencies(bool ascending)
        {
            var tarihDateCurrencies = GetCurrenciesList(_tarihDate.Currency);
            SortCurrencies(tarihDateCurrencies, ascending);
            AddDbAllCurrencies(tarihDateCurrencies);
            PrintCurrencies(tarihDateCurrencies);
        }

        private static void SortCurrencies(List<Tarih_DateCurrency> tarihDateCurrencies, bool ascending)
        {
            if (ascending) tarihDateCurrencies.Sort();
            else
            {
                tarihDateCurrencies.Sort();
                tarihDateCurrencies.Reverse();
            }
        }

        private static List<Tarih_DateCurrency> GetCurrenciesList(Tarih_DateCurrency[] arrayCurrencies)
        {
            var tarihDateCurrencies = new List<Tarih_DateCurrency>();
            foreach (var element in arrayCurrencies)
            {
                switch (element.Kod)
                {
                    case "USD":
                        tarihDateCurrencies.Add(element);
                        break;

                    case "EUR":
                        tarihDateCurrencies.Add(element);
                        break;

                    case "GBP":
                        tarihDateCurrencies.Add(element);
                        break;

                    case "CHF":
                        tarihDateCurrencies.Add(element);
                        break;

                    case "KWD":
                        tarihDateCurrencies.Add(element);
                        break;

                    case "SAR":
                        tarihDateCurrencies.Add(element);
                        break;

                    case "RUB":
                        tarihDateCurrencies.Add(element);
                        break;
                }
            }
            return tarihDateCurrencies;
        }

        private static void PrintCurrencies(List<Tarih_DateCurrency> currencies)
        {
            foreach (var element in currencies)
            {
                _logger.LogInformation($"TRY/{element.Kod} {element.ForexBuying}");
            }
        }

        private void PrintCurrencies2(List<Currency> currencies)
        {
            foreach (var element in currencies)
            {
                _logger.LogInformation($"TRY/{element.Kod} {element.Tarih} {element.ForexBuying}");
            }
        }

        private static async void AddDbAllCurrencies(List<Tarih_DateCurrency> currencies)
        {
            foreach (var element in currencies)
            {
                var currency = new Currency
                {
                    Tarih = _tarihDate.Tarih,
                    ForexBuying = element.ForexBuying,
                    Kod = element.Kod
                };
                AddAppDbContext(currency);
                await _appDbContext.SaveChangesAsync();
            }
        }

        private static async void AddAppDbContext(Currency currency)
        {
            foreach (var elemetCurrency in _appDbContext.Currencies)
            {
                if (elemetCurrency.Kod.Equals(currency.Kod) && elemetCurrency.Tarih.Equals(currency.Tarih))
                {
                    elemetCurrency.ForexBuying = currency.ForexBuying;
                    return;
                }
            }
            await _appDbContext.Currencies.AddAsync(currency);
        }

        private static void HardDeleteAppDbContext(Currency currency)
        {
            _appDbContext.Remove(currency);
        }

        public static List<Currency> GetDailyChanges(Currency currency)
        {
            List<Currency> currencies = new List<Currency>();
            foreach (var elementCurrency in _appDbContext.Currencies)
            {
                if (elementCurrency.Kod.Equals(currency.Kod))
                {
                    currencies.Add(elementCurrency);
                    //_logger.LogInformation($"TRY/{elementCurrency.Kod} {elementCurrency.Tarih} {elementCurrency.ForexBuying}");
                }
            }
            return currencies;
        }


    }

    #endregion
}
