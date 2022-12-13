﻿// All lines from line 1 to the device interface implementation region will be discarded by the project wizard when the template is used
// Required code must lie within the device implementation region
// The //ENDOFINSERTEDFILE tag must be the last but one line in this file

using ASCOM.DeviceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using ASCOM;
using ASCOM.HomeMade;
using ASCOM.Utilities;

class DeviceSafetyMonitor
{
    public string internetServer { get; set; }
    public string soloServer { get; set; }
    private int internetFail = 0;
    private bool _trace = false;
    public bool trace
    {
        get { return _trace; }
        set
        {
            Logger.trace = value; _trace = value;
        }
    }

    public double TempCloud { get; set; }
    public double minTemp { get; set; }
    public double maxTemp { get; set; }
    public double maxHumid { get; set; }
    public double tempOffset { get; set; }
    public bool limitTempHumid { get; set; }
    public bool limitLuminosity { get; set; }
    public double maxWind { get; set; }
    public double maxGust { get; set; }
    public bool UPS { get; set; }
    public bool Internet { get; set; }
    public string UPSURL { get; set; }
    public string UPSSearch { get; set; }
    public int rainSensor { get; set; }

    #region ISafetyMonitor Implementation
    public bool IsSafe
    {
        get
        {
            bool safe = true;
            try
            {
                Logger.LogMessage("ISafetyMonitor - Get");
                string error = "";
                List<DataItem> data = GetData();
                if (data.LastOrDefault().rain == 1)
                {
                    safe = false;
                    error += "Rain detected\n";
                }

                if (!data.LastOrDefault().internet && Internet)
                {
                    internetFail++;
                    if (internetFail >= 2)
                    {
                        safe = false;
                        error += "Internet unavailable\n";
                    }
                }
                else
                {
                    internetFail = 0;
                }

                if (!data.LastOrDefault().safe)
                {
                    safe = false;
                    error += "CW unsafe\n";
                }

                if (data.LastOrDefault().wind > maxWind)
                {
                    safe = false;
                    error += "High wind detected\n";
                }
                
                if (data.LastOrDefault().gust > maxGust)
                {
                    safe = false;
                    error += "High wind gusts detected\n";
                }

                if (data.LastOrDefault().UPS && UPS)
                {
                    safe = false;
                    error += "System running on UPS\n";
                }

                if (data.LastOrDefault().cloud > TempCloud)
                {
                    safe = false;
                    error += "Clouds detected (sky temp)\n";
                }
                if (data.LastOrDefault().temperature > maxTemp)
                {
                    safe = false;
                    error += "Temperature too high\n";
                }
                if (data.LastOrDefault().temperature < minTemp)
                {
                    safe = false;
                    error += "Temperature too low\n";
                }
                if (data.LastOrDefault().humidity > maxHumid)
                {
                    safe = false;
                    error += "Humidity too high\n";
                }
                if (limitTempHumid && ((data.LastOrDefault().temperature - tempOffset) < ((-0.0000188264 * Math.Pow(data.LastOrDefault().humidity, 3)) + (0.00317 * Math.Pow(data.LastOrDefault().humidity, 2)) + (0.038835 * data.LastOrDefault().humidity) - 16.7018)))
                {
                    safe = false;
                    error += "temp=" + data.LastOrDefault().temperature + ", offset=" + tempOffset + ", humidity="+ data.LastOrDefault().humidity + ", limit=" +
                             ((-0.0000188264 * Math.Pow(data.LastOrDefault().humidity, 3)) +
                              (0.00317 * Math.Pow(data.LastOrDefault().humidity, 2)) +
                              (0.038835 * data.LastOrDefault().humidity) - 16.7018)+"\n";
                    error += "Risk of mount freeze\n";
                }
                if (limitLuminosity && (data.LastOrDefault().light < 10000))
                {
                    safe = false;
                    error += "Too much light\n";
                }

                if (!String.IsNullOrEmpty(error))
                {
                    Logger.LogMessage(@"c:\temp\SafetyMonitor.error", "Unsafe. Reason: " + error);
                    Logger.LogMessage("Unsafe. Reason: " + error);
                }
            }
            catch (Exception e)
            {
                Logger.LogMessage(e.Message+"\n"+e.StackTrace+"\n");
            }
            return safe;
        }
    }

    #endregion

    private void LogMessage(string message)
    {
        Logger.trace = trace;
        Logger.LogMessage(message);
    }
    
    private List<DataItem> GetData()
    {
        List<DataItem> data = RemoteData.GetData(soloServer, internetServer, UPSURL, UPSSearch);

        DataItem dataItem = null;
        foreach(DataItem item in data)
        {
            if (dataItem == null) dataItem = item;
            else dataItem = dataItem + item;
        }
        dataItem = dataItem / data.Count;
        return new List<DataItem>() { dataItem };
    }

    //ENDOFINSERTEDFILE
}