using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data;
using Xamarin.Forms;

namespace TestProject.Views.DetailViews
{
    public partial class Login : ContentPage
    {
        public CustomerQuery _CustomerQuery;
        public CustomersDB _CustomerDB;

        public Login()
        {
            InitializeComponent();
            this.BackgroundImage = "background.png";
            this.Title = "Login";

            var username = new Entry { Placeholder = "Username", PlaceholderColor = Color.Gray, TextColor = Color.White };
            var password = new Entry { IsPassword = true, Placeholder = "Password" , PlaceholderColor = Color.Gray, TextColor = Color.White };
            var login = new Button { Text = "Login" };
            var lbl_signup = new Label { Text = "Not a Member? Click the button and sign up now!", TextColor = Color.White };
            var signup = new Button { Text = "Sign up" };

            signup.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new SignUp());
            };

            login.Clicked += (object sender, EventArgs e) =>
            {
                CustomersDB _CustomerDB = new CustomersDB();
                CustomerQuery _CustomerQuery = new CustomerQuery();



                CustomersDB c = _CustomerQuery.GetCustName(username.Text);
                if (c != null)
                {
                    if (password.Text == c.Password)
                    {
                        DisplayAlert("Alert", "login succesful", "OK");
                       Navigation.PushAsync(new PayNow());
                    }
                    else
                    {
                        DisplayAlert("Alert", "login failed", "OK");
                    }
                }
                //     DisplayAlert("Alert", "ID: " + c.Id + "  UserName: " + c.Name + " Password: " + c.Password, "OK");

            };

            //var btnStack = new StackLayout
            //{
            //    Orientation = StackOrientation.Horizontal,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    Children = {
            //       login, signup
            //    }
            //};

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children = { username, password, login, lbl_signup, signup }
            };
        }
    }
}
