using Newtonsoft.Json;
using Plugin.Connectivity;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestProject.Data;
using TestProject.Models;
using Xamarin.Forms;

namespace TestProject.Views.DetailViews
{
    
    public partial class JsonTable : ContentPage
    {
        public int resul;
        public  static int tablenew { get; set; }
        public int data;

        public JsonTable(int numb)
        {
            //InitializeComponent();
            InitializeComponent();
            this.BackgroundImage = "background.png";
            this.Title = "Tables";
            resul = numb;
           // DisplayAlert("identity", Convert.ToString(resul), "ok");


            GetJSON();

            BindingContext = new JsonTableClass();


            //int resul = numb;

            //DisplayAlert("identity", Convert.ToString(resul),"ok");
            // CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;   
            // decimal toti = 0;
            // ToolbarItem cartItem = new ToolbarItem();
            //// cartItem.Text = "My Cart";
            // cartItem.Order = ToolbarItemOrder.Primary;
            // cartItem.Icon = "ShoppingCart9.png";
            // cartItem.Command = new Command(() => Navigation.PushAsync(new OnlineCart(toti)));
            // ToolbarItems.Add(cartItem);

            //StuwaQuery n = new StuwaQuery();
            //SQLiteConnection k;
            //k = DependencyService.Get<ISQLite>().GetConnection();
            //k.Table<StuwardTbl>();
            //var cou = k.ExecuteScalar<string>("SELECT max(StuwaName) FROM StuwardTbl");
            //var stid = k.ExecuteScalar<int>("SELECT max(StuwaID) FROM StuwardTbl");

            //DisplayAlert("Stuwa Name", cou,Convert.ToString(stid), "OK");
        }       
        
        public void OnStartClicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new JsonParsingPage());
            //JsonTableone tablename = new JsonTableone();
            //var nam = 
                

            var data = (sender as Button).BindingContext as JsonTableClass;
            //var info = ((Button);
            tablenew = data.ID;

           //TempTbl tbr = new TempTbl();
           // SQLiteConnection s;
           // s = DependencyService.Get<ISQLite>().GetConnection();
           // s.Table<TempTbl>();
           // // s.DeleteAll<TempTbl>();
           // //TableName = (sender as Button).Text;
           // tbr.TblName = Convert.ToString(data.ID);
           // //tbr.TblName = Convert.ToString(data.ID);
           // TableQuery c = new TableQuery();
           // c.InsertDetails(tbr);
           // DisplayAlert("button", Convert.ToString(data.ID), "OK");
            decimal toti = 0 ;
            // Navigation.PushAsync(new OnlineTabbed());
            //  Navigation.PushAsync(new OnlineCart( toti));
         
            GetTicketinfo();

         
        }

        public async void GetJSON()
        {
            var client = new System.Net.Http.HttpClient();           
            var response = await client.GetAsync(Constants.BaseUrlpos+"TableDetails.php");
            string contactsJson = response.Content.ReadAsStringAsync().Result;
            JsonTableone ObjContactList = new JsonTableone();
            //if (contactsJson != "")
            if (response.IsSuccessStatusCode)
            {
                //Converting JSON Array Objects into generic list
                // await DisplayAlert("sucess", " Network Is Available.", "Ok");
                ObjContactList = JsonConvert.DeserializeObject<JsonTableone>(contactsJson);
                listviewTables.ItemsSource = ObjContactList.TableInfo;
            }
            

        }

        public async void GetTicketinfo()
        {
            //TableQuery p = new TableQuery();
            //SQLiteConnection r;
            //r = DependencyService.Get<ISQLite>().GetConnection();
            //r.Table<TempTbl>();
            //var count = r.ExecuteScalar<string>("SELECT max(TblName) FROM TempTbl");
           // await DisplayAlert("tableID", count, "OK");

            var TicketNu = 0;
            var FlagNu =0;

            string LocationId = "1";
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            
            postData.Add(new KeyValuePair<string, string>("LocationId", LocationId));
            postData.Add(new KeyValuePair<string, string>("TableID", Convert.ToString(tablenew)));////////
            postData.Add(new KeyValuePair<string, string>("IdentityID", Convert.ToString(resul)));
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
                // await Navigation.PushAsync(new OnlineTabbed());
                await Navigation.PushAsync(new StuwardSelect());
                }
                else if(FlagNu == 1)
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

        private void listviewTable_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var itemSelectedData = e.SelectedItem as JsonTableClass;
            //TempTbl tbr = new TempTbl();
            //SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //s.Table<TempTbl>();
            //// s.DeleteAll<TempTbl>();
            ////TableName = (sender as Button).Text;
            //tbr.TblName = itemSelectedData.Name;
            //TableQuery c = new TableQuery();
            //c.InsertDetails(tbr);

            //DisplayAlert("pressed", itemSelectedData.Name, "ok");


            //Navigation.PushAsync(new JsonDetailsPage(itemSelectedData.Image, itemSelectedData.Name, itemSelectedData.Description, itemSelectedData.Price));
        }

       
    }
}
