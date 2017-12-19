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
using TestProject.Views.Menu;
using Xamarin.Forms;

namespace TestProject.Views.DetailViews
{
    public partial class KOTsendPage : ContentPage
    {
        public KOTsendPage()
        {
            InitializeComponent();
            this.BackgroundImage = "background.png";
            this.Title = "COMPLETE ORDER";
        }
               
        public void Menuback_btn_Clickedone(object sender, EventArgs e)
        {
            GetJSON();
        }

        public void table_Clickedone(object sender, EventArgs e)
        {
            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            TempTbl tbr = new TempTbl();
            s.Table<TempTbl>();
            s.DeleteAll<TempTbl>();

            TicketQuery y = new TicketQuery();
            SQLiteConnection d;
            d = DependencyService.Get<ISQLite>().GetConnection();
            d.Table<TicketNumber>();
            d.DeleteAll<TicketNumber>();
            
            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            TotalTbl tota = new TotalTbl();
            r.Table<TotalTbl>();
            r.DeleteAll<TotalTbl>();


            Navigation.PushAsync(new Dashboard());
        }

        public async void GetJSON()
        {
            //Check network status 
            //if (NetworkCheck.IsNetworkConnected())          //{


            //TableQuery p = new TableQuery();
            //SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //s.Table<TempTbl>();
            //var count = s.ExecuteScalar<string>("SELECT TblName FROM TempTbl");

            TicketQuery y = new TicketQuery();
            SQLiteConnection d;
            d = DependencyService.Get<ISQLite>().GetConnection();
            d.Table<TicketNumber>();
            var tikcount = d.ExecuteScalar<string>("SELECT TicketNum FROM TicketNumber");
           // ticketnow.Text = tikcount;

           // string flag = "1";
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("TestTicket", tikcount.Replace("\r\n", "")));
          //  postData.Add(new KeyValuePair<string, string>("Flag", flag));

            var content = new FormUrlEncodedContent(postData);
            var response =  await client.PostAsync(Constants.BaseUrlpos + "KOTCheckNew.php", content);
            string contactsJson = response.Content.ReadAsStringAsync().Result;
            JsonCartone ObjContactList = new JsonCartone();
            if (response.IsSuccessStatusCode)
            {
                ObjContactList = JsonConvert.DeserializeObject<JsonCartone>(contactsJson);
                cartwo.ItemsSource = ObjContactList.CartDetails;
            }

        }

    }
}
