using Plugin.DeviceInfo;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestProject.Data;
using TestProject.Views;
using TestProject.Views.Menu;
using Xamarin.Forms;

namespace TestProject
{
    public partial class App : Application
    {
        static TokenDatabaseController tokendatabase;
        static UserDatabaseController userdatabase;
        static RestService resstService;

        public App()
        {
            InitializeComponent();

            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Device Unique ID:"
            //            },
            //            new Label{
            //                XAlign = TextAlignment.Center,
            //                Text = TestProject.Data.Hardware.Default.DeviceId
            //            }
            //        }
            //    }
            //};

            MainPage = new NavigationPage(new OnlineLoginPage());
           // OnlineLoginPage
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userdatabase == null)
                {
                    userdatabase = new UserDatabaseController();
                }
                return userdatabase;
            }
        }

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokendatabase == null)
                {
                    tokendatabase = new TokenDatabaseController();
                }
                return tokendatabase;
            }
        }

        public static RestService RestService
      {
           get
            {
             if ( resstService == null)
           {
                resstService = new RestService();
            }
            return resstService;
          }
       }

    }
}
