using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using ASCOM.HomeMade;
using Newtonsoft.Json;

namespace ASCOM.HomeMade
{
    public class DataItem
    {
        public int time;
        public double temperature;
        public double humidity;
        public double dew;
        public double pressure;
        public double cloud;
        public double rain;
        public double board;
        public double wind;
        public double gust;
        public double winddir;
        public double light;
        public bool safe;
        public bool internet;
        public bool UPS;

        public static DataItem operator+(DataItem b, DataItem c)
        {
            DataItem item = new DataItem();
            item.time = c.time;
            item.temperature = b.temperature + c.temperature;
            item.humidity = b.humidity + c.humidity;
            item.dew = b.dew + c.dew;
            item.pressure = b.pressure + c.pressure;
            item.cloud = b.cloud + c.cloud;
            item.rain = b.rain + c.rain;
            item.board = b.board + c.board;
            item.wind = b.wind + c.wind;
            item.gust = b.gust + c.gust;
            item.winddir = b.winddir + c.winddir;
            item.light = b.light + c.light;
            item.safe = b.safe && c.safe;
            item.internet = b.internet && c.internet;
            item.UPS = b.UPS && c.UPS;

            return item;
        }

        public static DataItem operator /(DataItem b, int c)
        {
            DataItem item = new DataItem();
            item.time = b.time;
            item.temperature = b.temperature / c;
            item.humidity = b.humidity / c;
            item.dew = b.dew / c;
            item.pressure = b.pressure / c;
            item.cloud = b.cloud / c;
            item.rain = b.rain / c;
            item.board = b.board / c;
            item.wind = b.wind / c;
            item.gust = b.gust / c;
            item.winddir = b.winddir / c;
            item.light = b.light / c;
            item.safe = b.safe;
            item.internet = b.internet;
            item.UPS = b.UPS;

            return item;
        }

    }

        public static class RemoteData
    {
        public static int UPDATEFREQUENCY = 60; // In seconds
        public static int NBAVERAGE = 1; // Multiples of UPDATEFREQUENCY
        private static List<DataItem> _Data = new List<DataItem>();
        private static DateTime _LastUpdate = DateTime.MinValue;
        private static int RainSensor = 0;

        private static Mutex semaphore = new Mutex();

        public static List<DataItem> GetData(string soloServer, string internetServer = "", string UPSURL = "", string UPSSearch = "", int rainSensor = 800)
        {
            RainSensor = rainSensor;
            try
            {
                Logger.LogMessage("Getting data");
                semaphore.WaitOne();
                bool soloError = false;
                if ((DateTime.Now - _LastUpdate).TotalSeconds > UPDATEFREQUENCY || _Data.Count == 0)
                {
                    DataItem di = new DataItem();

                    Logger.LogMessage("Loading SOLO data");
                    HttpClient _Client = new HttpClient();
                    Logger.LogMessage("URL is " + soloServer);

                    try
                    {
                        using (HttpResponseMessage response = _Client.GetAsync(soloServer).Result)
                        {
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    // ... Read the string.
                                    string result = content.ReadAsStringAsync().Result;
                                    Logger.LogMessage("Set Server result " + result);
                                    di = Decoder(result);
                                }
                            }
                            else
                            {
                                soloError = true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.LogMessage("Couldn't load SOLO data: "+e.Message+"\n"+e.StackTrace);
                        soloError = true;
                    }
                    if (soloError)
                    {
                        // Get the latest good data
                        di = _Data.LastOrDefault();
                    }

                    Logger.LogMessage("SOLO data loaded");
                    Logger.LogMessage("Testing internet");

                    di.internet = true;
                    if (!String.IsNullOrEmpty(internetServer))
                    {
                        _Client = new HttpClient();
                        Logger.LogMessage("URL is " + internetServer);

                        try
                        {
                            using (HttpResponseMessage response = _Client.GetAsync(internetServer).Result)
                            {
                                using (HttpContent content = response.Content)
                                {
                                    // ... Read the string.
                                    string result = content.ReadAsStringAsync().Result;
                                    Logger.LogMessage("Set Server result " + result);
                                    di.internet = true;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Thread.Sleep(2000);
                            // Try a second time just in case
                            try
                            {
                                using (HttpResponseMessage response = _Client.GetAsync(internetServer).Result)
                                {
                                    using (HttpContent content = response.Content)
                                    {
                                        // ... Read the string.
                                        string result = content.ReadAsStringAsync().Result;
                                        Logger.LogMessage("Set Server result " + result);
                                        di.internet = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                di.internet = false;
                            }

                        }
                    }
                    Logger.LogMessage("Internet tested");

                    Logger.LogMessage("Checking UPS");
                    _Client = new HttpClient();
                    Logger.LogMessage("URL is " + UPSURL);

                    try
                    {
                        using (HttpResponseMessage response = _Client.GetAsync(UPSURL).Result)
                        {
                            using (HttpContent content = response.Content)
                            {
                                // ... Read the string.
                                string result = content.ReadAsStringAsync().Result;
                                Logger.LogMessage("Set Server result " + result);
                                if (result.Contains(UPSSearch))
                                    di.UPS = false;
                                else
                                    di.UPS = true;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        di.UPS = false;
                    }
                    Logger.LogMessage("UPS checked");

                    _LastUpdate = DateTime.Now;
                    _Data.Add(di);
                    if (_Data.Count > NBAVERAGE)
                    {
                        List<DataItem> _Data2 = new List<DataItem>();
                        for (int i = 0; i < NBAVERAGE; i++) _Data2.Add(_Data.ElementAt(_Data.Count - i -1));
                        _Data = _Data2;
                    }
                    Logger.LogMessage("DataItem=" + JsonConvert.SerializeObject(di));
                }
                else
                {
                    Logger.LogMessage("Data is fresh enough, no need to fetch");
                }

                return _Data;
            }
            catch (Exception e)
            {
                Logger.LogMessage("Load data error " + e.ToString() + "\n" + e.StackTrace);
                throw new ASCOM.DriverException(e.Message);
            }
            finally
            {
                semaphore.ReleaseMutex();
            }

            return _Data;
        }

        private static DataItem Decoder(string data)
        {
            DataItem di = new DataItem();
            var epoch = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            di.time = (int)Math.Round(epoch);

            List<string> diList = data.Split('\n').ToList();
            foreach (string line in diList)
            {
                if (line.Contains("clouds=")) di.cloud = ConvertToDouble(line.Replace("clouds=", ""));
                if (line.Contains("temp=")) di.temperature = ConvertToDouble(line.Replace("temp=", ""));
                di.rain = 0.0;
                if (line.Contains("rain="))
                {
                    di.rain = ConvertToDouble(line.Replace("rain=", ""));
                }
                if (line.Contains("rainsensor=") && di.rain == 0.0)
                {
                    double r = ConvertToDouble(line.Replace("rainsensor=", ""));
                    if (r <= RainSensor) di.rain = 1;
                }
                if (line.Contains("wind=")) di.wind = ConvertToDouble(line.Replace("wind=", "")) / 3.6;
                if (line.Contains("gust=")) di.gust = ConvertToDouble(line.Replace("gust=", "")) / 3.6;
                if (line.Contains("light=")) di.light = ConvertToDouble(line.Replace("light=", ""));
                if (line.Contains("hum=")) di.humidity = ConvertToDouble(line.Replace("hum=", ""));
                if (line.Contains("dewp=")) di.dew = ConvertToDouble(line.Replace("dewp=", ""));
                if (line.Contains("pressure=")) di.pressure = ConvertToDouble(line.Replace("pressure=", ""));
                if (line.Contains("winddir=")) di.winddir = ConvertToDouble(line.Replace("winddir=", ""));
                if (line.Contains("safe=")) di.safe = (ConvertToDouble(line.Replace("safe=", ""))==1.0);
            }

            return di;
        }

        private static double ConvertToDouble(string num)
        {
            string a = Convert.ToString(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            num = num.Replace(".", a);
            num = num.Replace(",", a);
            return Double.Parse(num);
        }

        public static List<DataItem> GetDataAverage(string soloServer, string internetServer = "", string UPSURL = "", string UPSSearch = "", int rainSensor = 800)
        {
            List<DataItem> data = GetData(soloServer, internetServer, UPSURL, UPSSearch);

            DataItem dataItem = null;
            foreach (DataItem item in data)
            {
                if (dataItem == null) dataItem = item;
                else dataItem = dataItem + item;
            }
            dataItem = dataItem / data.Count;
            return new List<DataItem>() { dataItem };
        }
    }

}