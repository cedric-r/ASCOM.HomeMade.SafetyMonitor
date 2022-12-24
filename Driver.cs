//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM TEMPLATEDEVICECLASS driver for TEMPLATEDEVICENAME
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM TEMPLATEDEVICECLASS interface version: <To be completed by driver developer>
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define TEMPLATEDEVICECLASS

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace ASCOM.HomeMade
{
    //
    // Your driver's DeviceID is ASCOM.TEMPLATEDEVICENAME.TEMPLATEDEVICECLASS
    //
    // The Guid attribute sets the CLSID for ASCOM.TEMPLATEDEVICENAME.TEMPLATEDEVICECLASS
    // The ClassInterface/None addribute prevents an empty interface called
    // _TEMPLATEDEVICENAME from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM TEMPLATEDEVICECLASS Driver for TEMPLATEDEVICENAME.
    /// </summary>
    [Guid("07E9F8D9-E85C-4B2B-BC84-6F2EF6B3E781")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId(SafetyMonitor.driverID)]
    public class SafetyMonitor : ISafetyMonitor, IObservingConditions
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal const string driverID = "ASCOM.HomeMade.SafetyMonitor";
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM HomeMade Driver for SafetyMonitor and ObservingConditions.";
        private static int version = 1;

        internal static string comServerName = "InternetServer"; // Constants used for Profile persistence
        internal static string comServerDefault = "http://www.google.com";
        internal static string soloServerName = "SOLOServer"; // Constants used for Profile persistence
        internal static string soloServerDefault = "http://192.168.0.202/cgi-bin/cgiLastData";
        internal static string max25Name = "Max25"; // Constants used for Profile persistence
        internal static string max25Default = "false";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";
        internal static string tempCloudName = "TempCloud"; // Constants used for Profile persistence
        internal static string tempCloudDefault = "100";
        internal static string minTempName = "MinTemp"; // Constants used for Profile persistence
        internal static string minTempDefault = "-100";
        internal static string maxTempName = "MaxTemp"; // Constants used for Profile persistence
        internal static string maxTempDefault = "100";
        internal static string maxWindName = "MaxWind"; // Constants used for Profile persistence
        internal static string maxWindDefault = "100";
        internal static string maxGustName = "MaxGust"; // Constants used for Profile persistence
        internal static string maxGustDefault = "100";
        internal static string tempOffsetName = "TempOffset"; // Constants used for Profile persistence
        internal static string tempOffsetDefault = "0";
        internal static string limitTempHumidName = "LimitTempHumid"; // Constants used for Profile persistence
        internal static string limitTempHumidDefault = "0";
        internal static string limitLuminosityName = "LimitLuminosity"; // Constants used for Profile persistence
        internal static string limitLuminosityDefault = "0";
        internal static string UPSName = "UPS"; // Constants used for Profile persistence
        internal static string UPSDefault = "0";
        internal static string InternetName = "Internet"; // Constants used for Profile persistence
        internal static string InternetDefault = "0";
        internal static string UPSURLName = "UPSURL"; // Constants used for Profile persistence
        internal static string UPSURLDefault = "";
        internal static string UPSSearchName = "UPSSearch"; // Constants used for Profile persistence
        internal static string UPSSearchDefault = "";
        internal static string rainSensorName = "RainSensor"; // Constants used for Profile persistence
        internal static string rainSensorDefault = "800";
        internal static string maxHumidName = "MaxHumid"; // Constants used for Profile persistence
        internal static string maxHumidDefault = "100";
        internal static string avgName = "Avg"; // Constants used for Profile persistence
        internal static string avgDefault = "1";


        internal static string comServer; // Variables to hold the currrent device configuration
        internal static string soloServer; // Variables to hold the currrent device configuration
        internal static bool _trace = false;
        internal static double tempCloud = 100;
        internal static double minTemp = -100;
        internal static double maxTemp = 100;
        internal static double tempOffset = 0;
        internal static bool limitTempHumid = false;
        internal static bool limitLuminosity = false;
        internal static double maxWind = 15;
        internal static double maxGust = 20;
        internal static bool UPS = false;
        internal static bool Internet = false;
        internal static string UPSURL = "";
        internal static string UPSSearch = "";
        internal static int rainSensor = 800;
        internal static double maxHumid = 100;
        internal static int avg = 1;

        internal static bool trace { get { return _trace; } set
        {
            Logger.trace = value; _trace = value; }
        }
        internal static bool _Connected = false;

        private DeviceSafetyMonitor _safetymonitorcontroller = null;
        private DeviceObservingConditions _observingconditionscontroller = null;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="TEMPLATEDEVICENAME"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public SafetyMonitor()
        {
            Logger.LogMessage("Starting SafetyMonitor");
            _safetymonitorcontroller = new DeviceSafetyMonitor();
            _safetymonitorcontroller.internetServer = comServer;
            _safetymonitorcontroller.soloServer = soloServer;
            _safetymonitorcontroller.trace = trace;
            _safetymonitorcontroller.TempCloud = tempCloud;
            _safetymonitorcontroller.minTemp = minTemp;
            _safetymonitorcontroller.maxTemp = maxTemp;
            _safetymonitorcontroller.tempOffset = tempOffset;
            _safetymonitorcontroller.limitTempHumid = limitTempHumid;
            _safetymonitorcontroller.limitLuminosity = limitLuminosity;
            _safetymonitorcontroller.UPS = UPS;
            _safetymonitorcontroller.UPSURL = UPSURL;
            _safetymonitorcontroller.UPSSearch = UPSSearch;
            _safetymonitorcontroller.Internet = Internet;
            _safetymonitorcontroller.maxWind = maxWind;
            _safetymonitorcontroller.maxGust = maxGust;
            _safetymonitorcontroller.rainSensor = rainSensor;
            _safetymonitorcontroller.maxHumid = maxHumid;

            _observingconditionscontroller = new DeviceObservingConditions();
            _observingconditionscontroller.Server = soloServer;
            _observingconditionscontroller.trace = trace;

            RemoteData.NBAVERAGE = avg;


            ReadProfile(); // Read device configuration from the ASCOM Profile store

            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object
            //TODO: Implement your additional construction here
        }

        //
        // PUBLIC COM INTERFACE ITEMPLATEDEVICEINTERFACE IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
            // DO NOT have both these sections!  One or the other
        }

        public bool CommandBool(string command, bool raw)
        {
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
            // DO NOT have both these sections!  One or the other
        }

        public string CommandString(string command, bool raw)
        {
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            trace = false;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
        }

        public bool Connected
        {
            get
            {
                Logger.LogMessage("Connected - Get");
                return _Connected;
            }
            set
            {
                LogMessage("Connected", "Set {0}", value);

                _Connected = value;
            }
        }

        public string Description
        {
            // TODO customise this device description
            get
            {
                LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver des[cription
                LogMessage("Description Get", driverDescription);
                string driverInfo = driverDescription + " Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", version.ToString());
                return Convert.ToInt16(version);
            }
        }

        public string Name
        {
            get
            {
                string name = driverID;
                LogMessage("SafetyMonitor", "Name Get");
                return name;
            }
        }

        #endregion

        //INTERFACECODEINSERTIONPOINT
        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "SafetyMonitor";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }

                P.DeviceType = "ObservingConditions";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }

            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            try
            {
                using (Profile driverProfile = new Profile())
                {
                    driverProfile.DeviceType = "SafetyMonitor";
                    trace = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                    comServer = driverProfile.GetValue(driverID, comServerName, string.Empty, comServerDefault);
                    soloServer = driverProfile.GetValue(driverID, soloServerName, string.Empty, soloServerDefault);
                    tempCloud = Convert.ToDouble(driverProfile.GetValue(driverID, tempCloudName, string.Empty, tempCloudDefault));
                    minTemp = Convert.ToDouble(driverProfile.GetValue(driverID, minTempName, string.Empty, minTempDefault));
                    maxTemp = Convert.ToDouble(driverProfile.GetValue(driverID, maxTempName, string.Empty, maxTempDefault));
                    tempOffset = Convert.ToDouble(driverProfile.GetValue(driverID, tempOffsetName, string.Empty, tempOffsetDefault));
                    limitTempHumid = Convert.ToBoolean(driverProfile.GetValue(driverID, limitTempHumidName, string.Empty, limitTempHumidDefault));
                    limitLuminosity = Convert.ToBoolean(driverProfile.GetValue(driverID, limitLuminosityName, string.Empty, limitLuminosityDefault));
                    maxWind = Convert.ToDouble(driverProfile.GetValue(driverID, maxWindName, string.Empty, maxWindDefault));
                    maxGust = Convert.ToDouble(driverProfile.GetValue(driverID, maxGustName, string.Empty, maxGustDefault));
                    UPS = Convert.ToBoolean(driverProfile.GetValue(driverID, UPSName, string.Empty, UPSDefault));
                    Internet = Convert.ToBoolean(driverProfile.GetValue(driverID, InternetName, string.Empty, InternetDefault));
                    UPSURL = driverProfile.GetValue(driverID, UPSURLName, string.Empty, UPSURLDefault);
                    UPSSearch = driverProfile.GetValue(driverID, UPSSearchName, string.Empty, UPSSearchDefault);
                    rainSensor = Convert.ToInt32(driverProfile.GetValue(driverID, rainSensorName, string.Empty, rainSensorDefault));
                    maxHumid = Convert.ToDouble(driverProfile.GetValue(driverID, maxHumidName, string.Empty, maxHumidDefault));
                    avg = Convert.ToInt32(driverProfile.GetValue(driverID, avgName, string.Empty, avgDefault));

                    _safetymonitorcontroller.internetServer = comServer;
                    _safetymonitorcontroller.soloServer = soloServer;
                    _safetymonitorcontroller.trace = trace;
                    _safetymonitorcontroller.TempCloud = tempCloud;
                    _safetymonitorcontroller.minTemp = minTemp;
                    _safetymonitorcontroller.maxTemp = maxTemp;
                    _safetymonitorcontroller.tempOffset = tempOffset;
                    _safetymonitorcontroller.limitTempHumid = limitTempHumid;
                    _safetymonitorcontroller.limitLuminosity = limitLuminosity;
                    _safetymonitorcontroller.UPS = UPS;
                    _safetymonitorcontroller.UPSURL = UPSURL;
                    _safetymonitorcontroller.UPSSearch = UPSSearch;
                    _safetymonitorcontroller.Internet = Internet;
                    _safetymonitorcontroller.maxWind = maxWind;
                    _safetymonitorcontroller.maxGust = maxGust;
                    _safetymonitorcontroller.rainSensor = rainSensor;
                    _safetymonitorcontroller.maxHumid = maxHumid;
                    
                    _observingconditionscontroller.Server = soloServer;
                    _observingconditionscontroller.trace = trace;

                    RemoteData.NBAVERAGE = avg;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            try
            {
                using (Profile driverProfile = new Profile())
                {
                    driverProfile.DeviceType = "SafetyMonitor";
                    driverProfile.WriteValue(driverID, traceStateProfileName, trace.ToString());
                    driverProfile.WriteValue(driverID, comServerName, comServer.ToString());
                    driverProfile.WriteValue(driverID, soloServerName, soloServer.ToString());
                    driverProfile.WriteValue(driverID, tempCloudName, tempCloud.ToString());
                    driverProfile.WriteValue(driverID, minTempName, minTemp.ToString());
                    driverProfile.WriteValue(driverID, maxTempName, maxTemp.ToString());
                    driverProfile.WriteValue(driverID, tempOffsetName, tempOffset.ToString());
                    driverProfile.WriteValue(driverID, limitTempHumidName, limitTempHumid.ToString());
                    driverProfile.WriteValue(driverID, limitLuminosityName, limitLuminosity.ToString());
                    driverProfile.WriteValue(driverID, UPSName, UPS.ToString());
                    driverProfile.WriteValue(driverID, UPSURLName, UPSURL.ToString());
                    driverProfile.WriteValue(driverID, UPSSearchName, UPSSearch.ToString());
                    driverProfile.WriteValue(driverID, InternetName, Internet.ToString());
                    driverProfile.WriteValue(driverID, maxWindName, maxWind.ToString());
                    driverProfile.WriteValue(driverID, maxGustName, maxGust.ToString());
                    driverProfile.WriteValue(driverID, rainSensorName, rainSensor.ToString());
                    driverProfile.WriteValue(driverID, maxHumidName, maxHumid.ToString());
                    driverProfile.WriteValue(driverID, avgName, avg.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            Logger.trace = _trace;
            Logger.LogMessage(@"c:\temp\driver.log", identifier + ": "+msg);
        }

        #endregion

        #region ISafetyMonitor Members

        public bool IsSafe
        {
            get
            {
                Logger.LogMessage("IsSafe - Get");
                return _safetymonitorcontroller.IsSafe;
            }
        }
        #endregion

        #region IObservingConsitions Members
        //
        // Summary:
        //     Wind direction at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException The returned value
        //     must be between 0.0 and 360.0, interpreted according to the metereological standard,
        //     where a special value of 0.0 is returned when the wind speed is 0.0. Wind direction
        //     is measured clockwise from north, through east, where East=90.0, South=180.0,
        //     West=270.0 and North=360.0.
        public double WindDirection
        {
            get
            {
                Logger.LogMessage("WindDirection - Get");
                return _observingconditionscontroller.WindDirection;
            }
        }
        //
        // Summary:
        //     Temperature at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException
        //     The units of this property are degrees Celsius. Driver and application authors
        //     can use the ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     method to convert these units to and from degrees Farenhheit.
        //     This is expected to be the ambient temperature at the observatory.
        public double Temperature
        {
            get
            {
                Logger.LogMessage("Temperature - Get");
                return _observingconditionscontroller.Temperature;
            }
        }
        //
        // Summary:
        //     Sky temperature at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException
        //     The units of this property are degrees Celsius. Driver and application authors
        //     can use the ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     method to convert these units to and from degrees Farenhheit.
        //     This is expected to be returned by an infra-red sensor looking at the sky. The
        //     lower the temperature the more the sky is likely to be clear.
        public double SkyTemperature
        {
            get
            {
                Logger.LogMessage("SkyTemperature - Get");
                return _observingconditionscontroller.SkyTemperature;
            }
        }
        //
        // Summary:
        //     Seeing at the observatory measured as star full width half maximum (FWHM) in
        //     arc secs.
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException
        public double StarFWHM
        {
            get
            {
                Logger.LogMessage("StarFWHM - Get");
                return _observingconditionscontroller.StarFWHM;
            }
        }

        //
        // Summary:
        //     Sky quality at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException
        //     Sky quality is typically measured in units of magnitudes per square arc second.
        //     A sky quality of 20 magnitudes per square arc second means that the overall sky
        //     appears with a brightness equivalent to having 1 magnitude 20 star in each square
        //     arc second of sky.
        //     Examples of typical sky quality values were published by Sky and Telescope (http://www.skyandtelescope.com/astronomy-resources/rate-your-skyglow/)
        //     and, in slightly adpated form, are reproduced below:
        //     Sky Quality (mag/arcsec2) Description 22.0 By convention, this is often assumed
        //     to be the average brightness of a moonless night sky that's completely free of
        //     artificial light pollution. 21.0 This is typical for a rural area with a medium-sized
        //     city not far away. It's comparable to the glow of the brightest section of the
        //     northern Milky Way, from Cygnus through Perseus. 20.0 This is typical for the
        //     outer suburbs of a major metropolis. The summer Milky Way is readily visible
        //     but severely washed out. 19.0 Typical for a suburb with widely spaced single-family
        //     homes. It's a little brighter than a remote rural site at the end of nautical
        //     twilight, when the Sun is 12° below the horizon. 18.0 Bright suburb or dark urban
        //     neighborhood. It's also a typical zenith skyglow at a rural site when the Moon
        //     is full. The Milky Way is invisible, or nearly so. 17.0 Typical near the center
        //     of a major city. 13.0 The zenith skyglow at the end of civil twilight, roughly
        //     a half hour after sunset, when the Sun is 6° below the horizon. Venus and Jupiter
        //     are easy to see, but bright stars are just beginning to appear. 7.0 The zenith
        //     skyglow at sunrise or sunset
        public double SkyQuality
        {
            get
            {
                Logger.LogMessage("SkyQuality - Get");
                return _observingconditionscontroller.SkyQuality;
            }
        }
        //
        // Summary:
        //     Sky brightness at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException This property
        //     returns the sky brightness measured in Lux.
        //     Luminance Examples in Lux
        //     IlluminanceSurfaces illuminated by: 0.0001 luxMoonless, overcast night sky (starlight)
        //     0.002 luxMoonless clear night sky with airglow 0.27–1.0 luxFull moon on a clear
        //     night 3.4 luxDark limit of civil twilight under a clear sky 50 luxFamily living
        //     room lights (Australia, 1998) 80 luxOffice building hallway/toilet lighting 100
        //     luxVery dark overcast day 320–500 luxOffice lighting 400 luxSunrise or sunset
        //     on a clear day. 1000 luxOvercast day; typical TV studio lighting 10000–25000
        //     luxFull daylight (not direct sun) 32000–100000 luxDirect sunlight
        public double SkyBrightness
        {
            get
            {
                Logger.LogMessage("SkyBrightness - Get");
                return _observingconditionscontroller.SkyBrightness;
            }
        }
        //
        // Summary:
        //     Rain rate at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException
        //     The units of this property are millimetres per hour. Client and driver authors
        //     can use the method ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     to convert these units to and from inches per hour.
        //     This property can be interpreted as 0.0 = Dry any positive nonzero value = wet.
        //     Rainfall intensity is classified according to the rate of precipitation:
        //     Light rain — when the precipitation rate is less than 2.5 mm (0.098 in) per hour
        //     Moderate rain — when the precipitation rate is between 2.5 mm (0.098 in) and
        //     10 mm (0.39 in) per hour Heavy rain — when the precipitation rate is between
        //     10 mm (0.39 in) and 50 mm (2.0 in) per hour Violent rain — when the precipitation
        //     rate is > 50 mm (2.0 in) per hour
        public double RainRate
        {
            get
            {
                Logger.LogMessage("RainRate - Get");
                return _observingconditionscontroller.RainRate;
            }
        }
        //
        // Summary:
        //     Atmospheric pressure at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException
        //     The units of this property are hectoPascals. Client and driver authors can use
        //     the method ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     to convert these units to and from milliBar, mm of mercury and inches of mercury.
        //     This must be the pressure at the observatory altitude and not the adjusted pressure
        //     at sea level. Please check whether your pressure sensor delivers local observatory
        //     pressure or sea level pressure and, if it returns sea level pressure, adjust
        //     this to actual pressure at the observatory's altitude before returning a value
        //     to the client. The ASCOM.Utilities.Util.ConvertPressure(System.Double,System.Double,System.Double)
        //     method can be used to effect this adjustment.
        public double Pressure
        {
            get
            {
                Logger.LogMessage("Pressure - Get");
                return _observingconditionscontroller.Pressure;
            }
        }
        //
        // Summary:
        //     Atmospheric humidity at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException when the ASCOM.DeviceInterface.IObservingConditions.DewPoint
        //     property also throws a PropertyNotImplementedException. Mandatory property, must
        //     NOT throw a PropertyNotImplementedException when the ASCOM.DeviceInterface.IObservingConditions.DewPoint
        //     property is implemented.
        //     The ASCOM specification requires that DewPoint and Humidity are either both implemented
        //     or both throw PropertyNotImplementedExceptions. It is not allowed for one to
        //     be implemented and the other to throw a PropertyNotImplementedException. The
        //     Utilities component contains methods (ASCOM.Utilities.Util.DewPoint2Humidity(System.Double,System.Double)
        //     and ASCOM.Utilities.Util.Humidity2DewPoint(System.Double,System.Double)) to convert
        //     DewPoint to Humidity and vice versa given the ambient temperature.
        //     This property should return a value between 0.0 and 100.0 where 0.0 = 0% relative
        //     humidity and 100.0 = 100% relative humidity.
        public double Humidity
        {
            get
            {
                Logger.LogMessage("Humidity - Get");
                return _observingconditionscontroller.Humidity;
            }
        }
        //
        // Summary:
        //     Atmospheric dew point at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException when the ASCOM.DeviceInterface.IObservingConditions.Humidity
        //     property also throws a PropertyNotImplementedException. Mandatory property, must
        //     NOT throw a PropertyNotImplementedException when the ASCOM.DeviceInterface.IObservingConditions.Humidity
        //     property is implemented.
        //     The units of this property are degrees Celsius. Driver and application authors
        //     can use the ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     method to convert these units to and from degrees Farenhheit.
        //     The ASCOM specification requires that DewPoint and Humidity are either both implemented
        //     or both throw PropertyNotImplementedExceptions. It is not allowed for one to
        //     be implemented and the other to throw a PropertyNotImplementedException. The
        //     Utilities component contains methods (ASCOM.Utilities.Util.DewPoint2Humidity(System.Double,System.Double)
        //     and ASCOM.Utilities.Util.Humidity2DewPoint(System.Double,System.Double)) to convert
        //     DewPoint to Humidity and vice versa given the ambient temperature.
        public double DewPoint
        {
            get
            {
                Logger.LogMessage("DewPoint - Get");
                return _observingconditionscontroller.DewPoint;
            }
        }
        //
        // Summary:
        //     Amount of sky obscured by cloud
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException This property
        //     should return a value between 0.0 and 100.0 where 0.0 = clear sky and 100.0 =
        //     100% cloud coverage
        public double CloudCover
        {
            get
            {
                Logger.LogMessage("CloudCover - Get");
                return _observingconditionscontroller.CloudCover;
            }
        }
        //
        // Summary:
        //     Gets And sets the time period over which observations will be averaged
        //
        // Exceptions:
        //   T:ASCOM.InvalidValueException:
        //     If the value set is not available for this driver. All drivers must accept 0.0
        //     to specify that an instantaneous value is available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Mandatory property, must be implemented, can NOT throw a PropertyNotImplementedException
        //     This property should return the time period (hours) over which sensor readings
        //     will be averaged. If your driver is delivering instantaneous sensor readings
        //     this property should return a value of 0.0.
        //     Please resist the temptation to throw exceptions when clients query sensor properties
        //     when insufficient time has passed to get a true average reading. A best estimate
        //     of the average sensor value should be returned in these situations.
        public double AveragePeriod
        {
            get
            {
                Logger.LogMessage("AveragePeriod - Get");
                return _observingconditionscontroller.AveragePeriod;
            }
            set
            {
                Logger.LogMessage("AveragePeriod - Set");
                _observingconditionscontroller.AveragePeriod = value;
            }
        }

        //
        // Summary:
        //     Wind speed at the observatory
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException The units of this
        //     property are metres per second. Driver and application authors can use the ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     method to convert these units to and from miles per hour or knots.
        public double WindSpeed
        {
            get
            {
                Logger.LogMessage("WindSpeed - Get");
                return _observingconditionscontroller.WindSpeed;
            }
        }
        //
        // Summary:
        //     Peak 3 second wind gust at the observatory over the last 2 minutes
        //
        // Exceptions:
        //   T:ASCOM.PropertyNotImplementedException:
        //     If this property is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        // Remarks:
        //     Optional property, can throw a PropertyNotImplementedException The units of this
        //     property are metres per second. Driver and application authors can use the ASCOM.Utilities.Util.ConvertUnits(System.Double,ASCOM.Utilities.Units,ASCOM.Utilities.Units)
        //     method to convert these units to and from miles per hour or knots.
        public double WindGust
        {
            get
            {
                Logger.LogMessage("WindGust - Get");
                return _observingconditionscontroller.WindGust;
            }
        }
        //
        // Summary:
        //     Forces the driver to immediately query its attached hardware to refresh sensor
        //     values
        //
        // Exceptions:
        //   T:ASCOM.MethodNotImplementedException:
        //     If this method is not available.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected.
        //
        // Remarks:
        //     Optional method, can throw a MethodNotImplementedException
        public void Refresh()
        {
            Logger.LogMessage("Refresh");
            _observingconditionscontroller.Refresh();
        }
        //
        // Summary:
        //     Provides a description of the sensor providing the requested property
        //
        // Parameters:
        //   PropertyName:
        //     Name of the sensor whose description is required
        //
        // Returns:
        //     The description of the specified sensor.
        //
        // Exceptions:
        //   T:ASCOM.MethodNotImplementedException:
        //     If the sensor is not implemented.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        //   T:ASCOM.InvalidValueException:
        //     If an invalid property name parameter is supplied.
        //
        // Remarks:
        //     Must Not throw a MethodNotImplementedException when the specified sensor Is implemented
        //     but must throw a MethodNotImplementedException when the specified sensor Is Not
        //     implemented.
        //     PropertyName must be the name of one of the sensor properties specified in the
        //     ASCOM.DeviceInterface.IObservingConditions interface. If the caller supplies
        //     some other value, throw an InvalidValueException.
        //     If the sensor is implemented, this must return a valid string, even if the driver
        //     is not connected, so that applications can use this to determine what sensors
        //     are available.
        //     If the sensor is not implemented, this must throw a MethodNotImplementedException.
        public string SensorDescription(string PropertyName)
        {
            throw new MethodNotImplementedException("Method not implemented");
        }
        //
        // Summary:
        //     Provides the time since the sensor value was last updated
        //
        // Parameters:
        //   PropertyName:
        //     Name of the property whose time since last update is required
        //
        // Returns:
        //     Time in seconds since the last sensor update for this property
        //
        // Exceptions:
        //   T:ASCOM.MethodNotImplementedException:
        //     If the sensor is not implemented.
        //
        //   T:ASCOM.NotConnectedException:
        //     If the device is not connected and this information is only available when connected.
        //
        //   T:ASCOM.InvalidValueException:
        //     If an invalid property name parameter is supplied.
        //
        // Remarks:
        //     Must Not throw a MethodNotImplementedException when the specified sensor Is implemented
        //     but must throw a MethodNotImplementedException when the specified sensor Is Not
        //     implemented.
        //     PropertyName must be the name of one of the sensor properties specified in the
        //     ASCOM.DeviceInterface.IObservingConditions interface. If the caller supplies
        //     some other value, throw an InvalidValueException.
        //     Return a negative value to indicate that no valid value has ever been received
        //     from the hardware.
        //     If an empty string is supplied as the PropertyName, the driver must return the
        //     time since the most recent update of any sensor. A MethodNotImplementedException
        //     must not be thrown in this circumstance.
        public double TimeSinceLastUpdate(string PropertyName)
        {
            Logger.LogMessage("TimeSinceLastUpdate - Get");
            return _observingconditionscontroller.TimeSinceLastUpdate((PropertyName));
        }
        #endregion

    }
}
