using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Models;
using Xamarin.Forms;

namespace TestProject.Views.Menu
{
    public partial class OnlineLoginPage : ContentPage
    {
        public static string UserName { get; set; }
        public static int UserID { get; set; }

        public OnlineLoginPage()
        {
            InitializeComponent();
            Init();
            this.BackgroundImage = "background.png";
            this.Title = "Login";
        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            //Lbl_Username.TextColor = Constants.MainTextColor;
            // Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinnerone.IsVisible = false;
            LoginIconone.HeightRequest = Constants.LoginIconHeight;

            //App.StartCheckeIfInternet(lbl_NoInternet, this);

            Entry_Usernameone.Completed += (s, e) => Entry_Passwordone.Focus();
            Entry_Passwordone.Completed += (s, e) => SignInProcedureone(s, e);
        }
        

        async void SignInProcedureone(object sender, EventArgs e)
        {

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    //CrossConnectivity.Current.IsReachable
                    // Navigation.PushAsync(new JsonParsingPage());
                    //Navigation.PushAsync(new OnlineTabbed());
                    User user = new User(Entry_Usernameone.Text, Entry_Passwordone.Text);
                   // var uid = user.Id;
                    if (user.CheckInformation())
                    {
                        // await DisplayAlert("Login", "Login sucess", "Ok");
                        var result = await App.RestService.Login(user);
                        if (result.access_token != null)
                        {
                            // await DisplayAlert("USER LOGIN AS", result.access_token, "OK");
                            UserID =  result.Id;
                            UserName = Entry_Usernameone.Text.ToString();
                            // App.UserDatabase.SaveUser(user);
                            // App.TokenDatabase.SaveToken(result);

                            // await Navigation.PushAsync(new MasterDetail());

                            if (Device.OS == TargetPlatform.Android)
                            {
                                //Application.Current.MainPage = new NavigationPage(new Dashboard());
                                Application.Current.MainPage = new NavigationPage(new MasterDetail());
                            }
                            else if (Device.OS == TargetPlatform.iOS)
                            {
                                // await Navigation.PushModalAsync(new NavigationPage(new Dashboard()));
                                await Navigation.PushModalAsync(new NavigationPage(new MasterDetail()));
                            }
                        }
                        else
                        {
                            await DisplayAlert("Login1", "Login not correct, Empty username or password", "Ok");

                        }
                    }
                    else
                    {
                        await DisplayAlert("Login2", "Login not correct, Empty username or password", "Ok");
                    }

                }


                catch (Exception ex)
                {
                    // Console.WriteLine("An error occurred: '{0}'", ex);
                }
            }

            else
            {
                // write your code if there is no Internet available  
                //int y = 1;
                //var status = CrossConnectivity.Current.IsRemoteReachable("google.com");
                await DisplayAlert("NETWORK ERROR", "SYSTEM IS OFFLINE", "Ok");
                // Navigation.PushAsync(new MainTabbed());
            }

        }
    }
}
