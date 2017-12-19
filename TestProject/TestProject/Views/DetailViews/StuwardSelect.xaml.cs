using Newtonsoft.Json;
using Plugin.DeviceInfo;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data;
using TestProject.Models;
using Xamarin.Forms;

namespace TestProject.Views.DetailViews
{
    public partial class StuwardSelect : ContentPage
    {
        public static int StuwardIDn { get; set; }
        public static string StuwardNme { get; set; }

        public StuwardSelect()
        {
           
            //InitializeComponent();
            InitializeComponent();
            this.BackgroundImage = "background.png";
            this.Title = "Select Stuward";
            GetJSON();
            BindingContext = new JsonStuwardModel();

            var device  = Hardware.Default.DeviceId;          


        }

        public void OnStartClicked(object sender, EventArgs e)
        {
            int numb = 0;
            var data = (sender as Button).BindingContext as JsonStuwardModel;
            StuwardIDn = data.ID;
            StuwardNme = data.StName;

            //StuwardTbl tbr = new StuwardTbl();
            //SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //s.Table<StuwardTbl>();           
            //tbr.StuwaName = Convert.ToString(data.StName);
            //tbr.StuwaID = data.ID;
            //StuwaQuery c = new StuwaQuery();
            //c.InsertDetails(tbr);

            //StuwaQuery n = new StuwaQuery();
            //SQLiteConnection k;
            //k = DependencyService.Get<ISQLite>().GetConnection();
            //k.Table<StuwardTbl>();
            //var cou = k.ExecuteScalar<string>("SELECT max(StuwaName) FROM StuwardTbl");
            //var stid = k.ExecuteScalar<int>("SELECT max(StuwaID) FROM StuwardTbl");

           // DisplayAlert("Stuwa Name", cou, Convert.ToString(stid), "OK");
           // DisplayAlert("Stuwa Name", StuwardNme, Convert.ToString(StuwardIDn), "OK");

            Navigation.PushAsync(new OnlineTabbed());
            //Navigation.PushAsync(new JsonTable(numb));       


        }

        public async void GetJSON()
        {
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(Constants.BaseUrlpos+"StuwardDetail.php");
            string contactsJson = response.Content.ReadAsStringAsync().Result;
            JsonStuwardone ObjContactList = new JsonStuwardone();
            //if (contactsJson != "")
            if (response.IsSuccessStatusCode)
            {
                //Converting JSON Array Objects into generic list
                // await DisplayAlert("sucess", " Network Is Available.", "Ok");
                ObjContactList = JsonConvert.DeserializeObject<JsonStuwardone>(contactsJson);
                listviewStuward.ItemsSource = ObjContactList.StuwardInfo;
            }


        }

         

        private void listviewTable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }
    }
}
