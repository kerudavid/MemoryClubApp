using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using MemoryClubApp.Droid;
using Android.Net.Wifi;
using MemoryClubApp.Interfaces;

[assembly: Dependency(typeof(SignalDroid))]
namespace MemoryClubApp.Droid
{
    public class SignalDroid: IWifiSignal
    {
        public int GetStrenght()
        {
            Context _context = Android.App.Application.Context;


            WifiManager wifiManager = WifiManager.FromContext(Android.App.Application.Context);
            //wifiManager = (WifiManager)_context.GetSystemService(Context.WifiService);

            if (wifiManager == null)
            {
                return 2022;
            }

            int numberOfLevels = 5;

#pragma warning disable CS0618 // Type or member is obsolete
            WifiInfo wifiInfo =  wifiManager.ConnectionInfo;
#pragma warning restore CS0618 // Type or member is obsolete

            if (wifiInfo == null)
            {
                return 2022;
            }

#pragma warning disable CS0618 // Type or member is obsolete
            int level = WifiManager.CalculateSignalLevel(wifiInfo.Rssi, numberOfLevels);
#pragma warning restore CS0618 // Type or member is obsolete

            return level;

        }
    }
}