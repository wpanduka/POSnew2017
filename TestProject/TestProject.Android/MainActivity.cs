using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;
using Android.Util;

namespace TestProject.Droid
{
    [Activity(Label = "Specta.RestPOS.App", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private UnhandledExceptionEventHandler domainExceptionHandler = new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            //ConnectivityManager connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
            //NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
            //bool isOnline = networkInfo.IsConnected;

            //bool isWifi = networkInfo.Type == ConnectivityType.Wifi;
            //if (isWifi)
            //{
            //    //DisplayAlert("massage", "Connected", "ok");
            //    Log.Debug("tag","connected");
            //}
            //else
            //{
            //    Log.Debug("tag", "fail");
            //}
            /////////////////////////////////////////
            //AppDomain.CurrentDomain.UnhandledException += domainExceptionHandler;
           // AndroidEnvironment.UnhandledExceptionRaiser += Workbook_UnhandledExceptionRaiser;

        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                Log.Error("monoDroid_unhandedException, info=" + ex.Message + "\nstack=\n" + ex.StackTrace, "", "");
            }

            finally
            {
                Log.Error("monoDroid_unhandedException Finally\n", "", "");
            }
        }


        static void Workbook_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.Exception;
                e.Handled = true;
                Log.Error("UnhandledExceptionRaiser, info=" + ex.Message + "\nstack=\n" + ex.StackTrace,"");
            }
            finally
            {
                Log.Error("UnhandledExceptionRaiser Finally\n","");
            }
        }


        protected override void Dispose(bool disposing)
        {
            Log.Debug("MainActivity.Dispose()","");
            AppDomain.CurrentDomain.UnhandledException -= domainExceptionHandler;
            AndroidEnvironment.UnhandledExceptionRaiser -= Workbook_UnhandledExceptionRaiser;
            base.Dispose(disposing);
        }
    }
}

