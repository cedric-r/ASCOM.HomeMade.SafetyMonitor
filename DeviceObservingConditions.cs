// All lines from line 1 to the device interface implementation region will be discarded by the project wizard when the template is used
// Required code must lie within the device implementation region
// The //ENDOFINSERTEDFILE tag must be the last but one line in this file

using ASCOM;
using ASCOM.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using ASCOM.HomeMade;
using Newtonsoft.Json;

class DeviceObservingConditions
{
    //private static TraceLogger tl = new TraceLogger();
    private bool _trace = false;
    public bool trace
    {
        get { return _trace; }
        set
        {
            Logger.trace = value; _trace = value;
        }
    }
    public string Server { get; set; }
    private List<DataItem> _Data = new List<DataItem>();
    private DateTime _LastUpdate = DateTime.MinValue;

    private void LogMessage(string message)
    {
        if (trace)
            File.AppendAllText(@"c:\temp\ObservingConditions.log", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message + "\n");
    }

    #region IObservingConditions Implementation

    /// <summary>
    /// Gets and sets the time period over which observations wil be averaged
    /// </summary>
    /// <remarks>
    /// Get must be implemented, if it can't be changed it must return 0
    /// Time period (hours) over which the property values will be averaged 0.0 =
    /// current value, 0.5= average for the last 30 minutes, 1.0 = average for the
    /// last hour
    /// </remarks>
    public double AveragePeriod
    {
        get
        {
            LogMessage("AveragePeriod: get");
            return RemoteData.UPDATEFREQUENCY*RemoteData.NBAVERAGE/60/60;
        }
        set
        {
            LogMessage("AveragePeriod: set " + value);
            if (value != 0)
                throw new InvalidValueException("AveragePeriod", value.ToString(), "0 only");
        }
    }

    /// <summary>
    /// Amount of sky obscured by cloud
    /// </summary>
    /// <remarks>0%= clear sky, 100% = 100% cloud coverage</remarks>
    public double CloudCover
    {
        get
        {
            LogMessage("CloudCover: get");
            if (GetData().Last().temperature - GetData().Last().cloud > 20) return 0.0;
            if (GetData().Last().temperature - GetData().Last().cloud >= 18) return 50.0;
            if (GetData().Last().temperature - GetData().Last().cloud < 18) return 100.0;
            return 100.0;
        }
    }

    /// <summary>
    /// Atmospheric dew point at the observatory in deg C
    /// </summary>
    /// <remarks>
    /// Normally optional but mandatory if <see cref=" ASCOM.DeviceInterface.IObservingConditions.Humidity"/>
    /// Is provided
    /// </remarks>
    public double DewPoint
    {
        get
        {
            LogMessage("DewPoint: get");
            List<DataItem> data = GetData();
            return data.Last().dew;
        }
    }

    /// <summary>
    /// Atmospheric relative humidity at the observatory in percent
    /// </summary>
    /// <remarks>
    /// Normally optional but mandatory if <see cref="ASCOM.DeviceInterface.IObservingConditions.DewPoint"/> 
    /// Is provided
    /// </remarks>
    public double Humidity
    {
        get
        {
            LogMessage("Humidity: get");
            List<DataItem> data = GetData();
            return data.Last().humidity;
        }
    }

    /// <summary>
    /// Atmospheric pressure at the observatory in hectoPascals (mB)
    /// </summary>
    /// <remarks>
    /// This must be the pressure at the observatory and not the "reduced" pressure
    /// at sea level. Please check whether your pressure sensor delivers local pressure
    /// or sea level pressure and adjust if required to observatory pressure.
    /// </remarks>
    public double Pressure
    {
        get
        {
            LogMessage("Pressure: get");
            List<DataItem> data = GetData();
            return data.Last().pressure;
        }
    }

    /// <summary>
    /// Rain rate at the observatory
    /// </summary>
    /// <remarks>
    /// This property can be interpreted as 0.0 = Dry any positive nonzero value
    /// = wet.
    /// </remarks>
    public double RainRate
    {
        get
        {
            LogMessage("RainRate: get");
            List<DataItem> data = GetData();
            return data.Last().rain;
        }
    }

    /// <summary>
    /// Forces the driver to immediatley query its attached hardware to refresh sensor
    /// values
    /// </summary>
    public void Refresh()
    {
        GetData();
    }

    /// <summary>
    /// Provides a description of the sensor providing the requested property
    /// </summary>
    /// <param name="PropertyName">Name of the property whose sensor description is required</param>
    /// <returns>The sensor description string</returns>
    /// <remarks>
    /// PropertyName must be one of the sensor properties, 
    /// properties that are not implemented must throw the MethodNotImplementedException
    /// </remarks>
    public string SensorDescription(string PropertyName)
    {
        switch (PropertyName.Trim().ToLowerInvariant())
        {
            case "averageperiod":
                return "Average period in hours, immediate values are only available";
            case "dewpoint":
            case "humidity":
            case "pressure":
            case "rainrate":
            case "skybrightness":
            case "skyquality":
            case "starfwhm":
            case "skytemperature":
            case "temperature":
            case "winddirection":
            case "windgust":
            case "windspeed":
            default:
                LogMessage("SensorDescription " + PropertyName + " - not implemented");
                throw new ASCOM.InvalidValueException("SensorDescription(" + PropertyName + ")");
        }
    }

    /// <summary>
    /// Sky brightness at the observatory
    /// </summary>
    public double SkyBrightness
    {
        get
        {
            LogMessage("SkyBrightness: get");
            List<DataItem> data = GetData();
            return data.Last().light;
        }
    }

    /// <summary>
    /// Sky quality at the observatory
    /// </summary>
    public double SkyQuality
    {
        get
        {
            LogMessage("SkyQuality: get - not implemented");
            throw new PropertyNotImplementedException("SkyQuality", false);
        }
    }

    /// <summary>
    /// Seeing at the observatory
    /// </summary>
    public double StarFWHM
    {
        get
        {
            LogMessage("StarFWHM: get - not implemented");
            throw new PropertyNotImplementedException("StarFWHM", false);
        }
    }

    /// <summary>
    /// Sky temperature at the observatory in deg C
    /// </summary>
    public double SkyTemperature
    {
        get
        {
            LogMessage("SkyTemperature: get");
            List<DataItem> data = GetData();
            return data.Last().cloud;
        }
    }

    /// <summary>
    /// Temperature at the observatory in deg C
    /// </summary>
    public double Temperature
    {
        get
        {
            LogMessage("Temperature: get");
            List<DataItem> data = GetData();
            return data.LastOrDefault().temperature;

        }
    }

    /// <summary>
    /// Provides the time since the sensor value was last updated
    /// </summary>
    /// <param name="PropertyName">Name of the property whose time since last update Is required</param>
    /// <returns>Time in seconds since the last sensor update for this property</returns>
    /// <remarks>
    /// PropertyName should be one of the sensor properties Or empty string to get
    /// the last update of any parameter. A negative value indicates no valid value
    /// ever received.
    /// </remarks>
    public double TimeSinceLastUpdate(string PropertyName)
    {
        // return the time
        LogMessage("TimeSinceLastUpdate for " + PropertyName);
        return (DateTime.Now - _LastUpdate).TotalSeconds;
    }

    /// <summary>
    /// Wind direction at the observatory in degrees
    /// </summary>
    /// <remarks>
    /// 0..360.0, 360=N, 180=S, 90=E, 270=W. When there Is no wind the driver will
    /// return a value of 0 for wind direction
    /// </remarks>
    public double WindDirection
    {
        get
        {
            LogMessage("WindDirection: get");
            List<DataItem> data = GetData();
            return data.Last().winddir;
        }
    }

    /// <summary>
    /// Peak 3 second wind gust at the observatory over the last 2 minutes in m/s
    /// </summary>
    public double WindGust
    {
        get
        {
            LogMessage("WindGust: get");
            List<DataItem> data = GetData();
            return data.Last().gust;
        }
    }

    /// <summary>
    /// Wind speed at the observatory in m/s
    /// </summary>
    public double WindSpeed
    {
        get
        {
            LogMessage("WindSpeed: get");
            List<DataItem> data = GetData();
            return data.Last().wind;
        }
    }

    #endregion

    #region private methods

    #region calculate the gust strength as the largest wind recorded over the last two minutes

    // save the time and wind speed values
    private Dictionary<DateTime, double> winds = new Dictionary<DateTime, double>();

    private double gustStrength;

    private void UpdateGusts(double speed)
    {
        Dictionary<DateTime, double> newWinds = new Dictionary<DateTime, double>();
        var last = DateTime.Now - TimeSpan.FromMinutes(2);
        winds.Add(DateTime.Now, speed);
        var gust = 0.0;
        foreach (var item in winds)
        {
            if (item.Key > last)
            {
                newWinds.Add(item.Key, item.Value);
                if (item.Value > gust)
                    gust = item.Value;
            }
        }
        gustStrength = gust;
        winds = newWinds;
    }

    #endregion

    private List<DataItem> GetData()
    {
        List<DataItem> data = RemoteData.GetDataAverage(Server);
        return data;
    }
    #endregion

    //ENDOFINSERTEDFILE
}