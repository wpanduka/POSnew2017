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
using Xamarin.Forms;

namespace TestProject.Views.DetailViews
{
    public partial class CartTiketsPage : ContentPage
    {
        public int thiTable;

        public CartTiketsPage()
        {
           

            InitializeComponent();
            Init();
            this.BackgroundImage = "background.png";
            thiTable = JsonTable.tablenew;
            GetGroupTickets();


            try
            {
                int newnumber = 2;
                ToolbarItem cartItem = new ToolbarItem();
                cartItem.Order = ToolbarItemOrder.Primary;
                cartItem.Text = "START NEW ORDER";
                // cartItem.Icon = "orderNewThree.png";

                cartItem.Command = new Command(() =>  tabletriger());
               // cartItem.Command = new Command(() => Navigation.PushAsync(new tabletriger()));
                ToolbarItems.Add(cartItem);

                //SQLiteConnection m;
                //m = DependencyService.Get<ISQLite>().GetConnection();
                //TempTbl mit = new TempTbl();
                //m.Table<TempTbl>();
                //m.DeleteAll<TempTbl>();
            }
            catch (Exception ex)
            {
                // Console.WriteLine("An error occurred: '{0}'", ex);
            }

        }
        public void tabletriger( )
        {
           
            GetTicketinfo();

        }


        public async void GetTicketinfo()
        {
            TableQuery p = new TableQuery();
            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            r.Table<TempTbl>();
            var count = r.ExecuteScalar<string>("SELECT max(TblName) FROM TempTbl");
            // await DisplayAlert("tableID", count, "OK");

            var TicketNu = 0;
            var FlagNu = 0;

            string LocationId = "1";
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("LocationId", LocationId));
            postData.Add(new KeyValuePair<string, string>("TableID", Convert.ToString(JsonTable.tablenew)));////////
            postData.Add(new KeyValuePair<string, string>("IdentityID", Convert.ToString(2)));
            var content = new FormUrlEncodedContent(postData);
            // var response = await client.PostAsync("http://192.168.43.226/GetTicketNew.php", content);
            var response = await client.PostAsync(Constants.BaseUrlpos + "GetTicketNew.php", content);

            JsonTicketNewNum ObjContactList = new JsonTicketNewNum();
            if (response.IsSuccessStatusCode)
            {
                string JsonTiket = response.Content.ReadAsStringAsync().Result;
                ObjContactList = JsonConvert.DeserializeObject<JsonTicketNewNum>(JsonTiket);
                foreach (TiketViewModel t in ObjContactList.TicketInfo)
                {
                    TicketNu = t.TiketNumb;
                    FlagNu = t.FlagNum;
                }
            }

            //  await DisplayAlert("ticket", Convert.ToString(TicketNu), "OK");
            TicketNumber tik = new TicketNumber();
            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            s.Table<TicketNumber>();
            tik.TicketNum = Convert.ToString(TicketNu);
            //tbr.TblName = Convert.ToString(data.ID);
            TicketQuery c = new TicketQuery();
            c.InsertDetails(tik);
            decimal toti = 0;

            if (FlagNu == 0)
            {
                //await DisplayAlert("ticket", Convert.ToString(TicketNu), "OK");
                await Navigation.PushAsync(new OnlineTabbed());
            }
            else if (FlagNu == 1)
            {
                //  await DisplayAlert("ticket", Convert.ToString(TicketNu), "OK");                    
                await Navigation.PushAsync(new OnlineCart(toti));
            }
            else
            {
                //  await DisplayAlert("ticket", Convert.ToString(TicketNu), "OK");
                await Navigation.PushAsync(new CartTiketsPage());
            }

        }


        void Init()
        {
            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            TicketNumber tota = new TicketNumber();
            r.Table<TicketNumber>();
            r.DeleteAll<TicketNumber>();
        }

        public void OnTicketClicked(object sender, EventArgs e)
        {
            var GroupTik = (sender as Button).BindingContext as TicketGroupModel;

            

            TicketNumber tik = new TicketNumber();
            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            s.Table<TicketNumber>();
            tik.TicketNum = Convert.ToString(GroupTik.TicketID);
            //tbr.TblName = Convert.ToString(data.ID);
            TicketQuery c = new TicketQuery();
            c.InsertDetails(tik);

            TicketQuery y = new TicketQuery();
            SQLiteConnection d;
            d = DependencyService.Get<ISQLite>().GetConnection();
            d.Table<TicketNumber>();
            var tikcount = s.ExecuteScalar<string>("SELECT TicketNum FROM TicketNumber");

           // DisplayAlert("ticket", tikcount, "ok");

            // GroupTik.TicketID;
            decimal toti = 0;           
            Navigation.PushAsync(new OnlineCart( toti));


        }

        public async void GetGroupTickets()
        {
            TableQuery p = new TableQuery();/////
            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            r.Table<TempTbl>();
            var count = r.ExecuteScalar<string>("SELECT TblName FROM TempTbl");

            //var TicketNu = 0;
            //var FlagNu = 0;

            string LocationId = "1";
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("LocationId", LocationId));
            postData.Add(new KeyValuePair<string, string>("TableID", Convert.ToString(thiTable)));////////
            var content = new FormUrlEncodedContent(postData);
            var response = await client.PostAsync(Constants.BaseUrlpos + "TicketGroupGet.php", content);
            string contactsJson = response.Content.ReadAsStringAsync().Result;

            TicketGroup ObjContactList = new TicketGroup();
            if (response.IsSuccessStatusCode)
            {
                ObjContactList = JsonConvert.DeserializeObject<TicketGroup>(contactsJson);
                listviewTicketGroup.ItemsSource = ObjContactList.TicketGroupInfo;
            }
        }

        private void listviewTable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
        }



    }
}
