using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data;
using TestProject.Models;
using TestProject.Views.DetailViews;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace TestProject.Views.Menu
{
    public partial class Dashboard : ContentPage
    {
        HttpClient client;

        public Dashboard()
        {
            InitializeComponent();
            Init();
            this.BackgroundImage = "background.png";
            this.Icon = "logo.png";
            //this.Title = "Custom Title";
            
           string name = OnlineLoginPage.UserName;
            this.Title = "YOU ARE LOGGED IN AS " + " " + name;

            

        }

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;           


        }

        public async Task<T> PostResponseLogin<T>(string weburl, FormUrlEncodedContent content) where T : class
        {
            client = new HttpClient();
            var response = await client.PostAsync(weburl, content);
            var jasonResult = response.Content.ReadAsStringAsync().Result;
            var responseObject = JsonConvert.DeserializeObject<T>(jasonResult);
            return responseObject;
        }

        async void SelectedScreen1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Infoscreen());
        }

        //public void Login_btn_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new Login());

        //}

        //public void Meals_btn_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new Meals());
        //}

        //public void Item_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new StuwardSelect());
        //}

        public void Cart_btn_Clicked(object sender, EventArgs e)
        {
            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            TempTbl tbr = new TempTbl();
            s.Table<TempTbl>();
            s.DeleteAll<TempTbl>();

            SQLiteConnection y;
            y = DependencyService.Get<ISQLite>().GetConnection();
            TicketNumber Ti = new TicketNumber();
            y.Table<TicketNumber>();
            y.DeleteAll<TicketNumber>();

            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            TotalTbl tota = new TotalTbl();
            r.Table<TotalTbl>();
            r.DeleteAll<TotalTbl>();

            SQLiteConnection v;
            v = DependencyService.Get<ISQLite>().GetConnection();
            StuwardTbl knr = new StuwardTbl();
            v.Table<StuwardTbl>();
            v.DeleteAll<StuwardTbl>();
        }

        public void table_Clicked(object sender, EventArgs e)
        {
            

            Navigation.PushAsync(new ChangeLanguagePage());
        }

        public void test_Clicked(object sender, EventArgs e)
        {
            int numb = 0;
            try
            {
                
                SQLiteConnection s;
                s = DependencyService.Get<ISQLite>().GetConnection();
                TempTbl tbr = new TempTbl();
                s.Table<TempTbl>();
                s.DeleteAll<TempTbl>();

                SQLiteConnection y;
                y = DependencyService.Get<ISQLite>().GetConnection();
                TicketNumber Ti = new TicketNumber();
                y.Table<TicketNumber>();
                y.DeleteAll<TicketNumber>();

                SQLiteConnection r;
                r = DependencyService.Get<ISQLite>().GetConnection();
                TotalTbl tota = new TotalTbl();
                r.Table<TotalTbl>();
                r.DeleteAll<TotalTbl>();

                SQLiteConnection v;
                v = DependencyService.Get<ISQLite>().GetConnection();
                StuwardTbl knr = new StuwardTbl();
                v.Table<StuwardTbl>();
                v.DeleteAll<StuwardTbl>();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("An error occurred: '{0}'", ex);
            }
            // Navigation.PushAsync(new TableNew());

            // Navigation.PushAsync(new StuwardSelect());
            Navigation.PushAsync(new JsonTable(numb));
        }

        public void onlinetab_Clicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new JsonParsingPage());
            // Navigation.PushAsync(new OnlineTabbed());
            Close_App();

        }

        //public void onlinecart_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FanteasticFourPromo());
        //}       

        //within my Methods_Android class within Android app solution
        public void Close_App()
        {
           // Navigation.PushAsync(new MasterDetail());
        //    Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

        //public void ticket_Clicked(object sender, EventArgs e)
        //{
        //    //SQLiteConnection s;
        //    //s = DependencyService.Get<ISQLite>().GetConnection();
        //    //TicketNumber Ti = new TicketNumber();           
        //    //s.Table<TicketNumber>();
        //    //s.DeleteAll<TicketNumber>();

        //}


        //public void onlinecart_Clicked(object sender, EventArgs e)
        //{
        //    // Navigation.PushAsync(new JsonParsingPage());
        //    Navigation.PushAsync(new OnlineCart());
        //}

        //public void KOTnow_Clicked(object sender, EventArgs e)
        //{
        //    // Navigation.PushAsync(new JsonParsingPage());
        //    // Navigation.PushAsync(new OnlineTabbed());
        //    Navigation.PushAsync(new KOTsendPage());

        //}

    }


}
