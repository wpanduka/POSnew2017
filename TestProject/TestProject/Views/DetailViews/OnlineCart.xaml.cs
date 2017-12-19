using Newtonsoft.Json;
using Plugin.Connectivity;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public partial class OnlineCart : ContentPage
    {
        public decimal newtot;
        public decimal tax = 15;
        ObservableCollection<JsonCartone> update_Item = new ObservableCollection<JsonCartone>();
        ObservableCollection<JsonCartone> itemsList = new ObservableCollection<JsonCartone>();
       // public decimal totalnav;

       // public List<JsonCartone> Peoples { get; set; }

        private JsonCartone itemCartList = new JsonCartone();


        //public Command DeleteCommand { get; }

        public OnlineCart(decimal totalnew)
        {
            InitializeComponent();
            this.BackgroundImage = "background.png";
            NavigationPage.SetHasBackButton(this, false);
            GC.Collect(1);

            try
            {
                int newnumber = 2;
                ToolbarItem cartItem = new ToolbarItem();
                cartItem.Order = ToolbarItemOrder.Primary;
                cartItem.Text = "START NEW ORDER";
               // cartItem.Icon = "orderNewThree.png";
                
                cartItem.Command = new Command(() => Navigation.PushAsync(new JsonTable(newnumber)));
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


            try
            {
               // int newnumber = 2;
                ToolbarItem RefreshPage = new ToolbarItem();
                RefreshPage.Order = ToolbarItemOrder.Primary;
                RefreshPage.Text = "REFRESH";
                // cartItem.Icon = "orderNewThree.png";
                //decimal tot;
                RefreshPage.Command = new Command(() => Navigation.PushAsync(new OnlineCart(totalnew)));
                ToolbarItems.Add(RefreshPage);

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
            // this.ToolbartItems.Add(new ToolbarItem { Text = "BTN 1", Icon = "myicon.png" });
            //CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            //var items = new List<JsonCartone>();
            //cartone.ItemsSource = items;
            //decimal tot;
            TotalTbl tbr = new TotalTbl();
            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            s.Table<TotalTbl>();
           // tbr.ItmID = id;
            tbr.ItmTotal = totalnew;
            //tbr.TblName = Convert.ToString(data.ID);
            TotalQuery c = new TotalQuery();
            c.InsertDetails(tbr);
           // this.total = totalnav;
           

            // decimal tot ;
            // tot =+ totalnew ;
            // total.Text = c.GetTotal() + "";
           // decimal tax = 15;
            total.Text = c.GetTotal() + "";
            decimal tota = Convert.ToDecimal(total.Text);
           // gandtot.Text = Convert.ToString(tota + tax);
           // ItemInfo.Text += Convert.ToString(id);
           // cartone.SelectedItem += ItemInfo.Text;
            cartone.ItemsSource = update_Item;

            // DisplayAlert("item IDs", ItemInfo.Text, "OK");

            // cartone.IsPullToRefreshEnabled = true;
            GetJSON();

            //long freeMemory = Java.Lang.Runtime.GetRuntime().FreeMemory();
            //DisplayAlert("freemomory", Convert.ToString(freeMemory), "OK");
        }

        protected override void OnDisappearing()
        {
            // ImagesStackLayout.Children.Clear();
            GC.Collect();
        }


        //private async void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        //{
        //    if (!e.IsConnected)
        //    {
        //        await DisplayAlert("fail", "No Internet Connection. Offline Menu activated", "Ok");
        //        await Navigation.PushAsync(new MainTabbed());
        //    }
        //    else
        //    {
        //        // await DisplayAlert("sucess", " Network Is Available.", "Ok");
        //        // sendJSON();
        //        GetJSON();
        //    }
        //}



        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (!CrossConnectivity.Current.IsConnected)
        //    {
        //        await DisplayAlert("fail", "No Internet Connection.Offline Menu Activated", "Ok");
        //        await Navigation.PushAsync(new MainTabbed());

        //    }
        //    else
        //    {
        //        // await DisplayAlert("sucess", " Network Is Available.", "Ok");
        //        // sendJSON();
        //        GetJSON();
        //    }
        //}

        //public ObservableCollection<JsonCartone> YourList
        //{
        //    get
        //    {
        //        return itemsList;
        //    }
        //    set
        //    {
        //        itemsList = value;

        //        //RaisePropertyChanged();
        //        //NotifyPropertyChanged("YourList");
        //    }
        //}


        public async void GetJSON()
        {
            //TableQuery p = new TableQuery();
            //SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //s.Table<TempTbl>();
            //var count = s.ExecuteScalar<string>("SELECT max(TblName) FROM TempTbl");
          //  tablenow.Text = count;

            TicketQuery y = new TicketQuery();
            SQLiteConnection d;
            d = DependencyService.Get<ISQLite>().GetConnection();
            d.Table<TicketNumber>();
            var tikcount = d.ExecuteScalar<string>("SELECT max(TicketNum) FROM TicketNumber");
           // ticketnow.Text = tikcount;

            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();

            //  postData.Add(new KeyValuePair<string, string>("TestTicket", tikcount.Replace("\r\n", "")));
            postData.Add(new KeyValuePair<string, string>("TestTicket",tikcount));
            var content = new FormUrlEncodedContent(postData);
           // var response = await client.PostAsync("http://192.168.43.226/cardorderGetNew.php", content);
            var response = await client.PostAsync(Constants.BaseUrlpos + "cardorderGetNew.php", content);
            string contactsJson = response.Content.ReadAsStringAsync().Result;
            JsonCartone ObjContactList = new JsonCartone();
            if (response.IsSuccessStatusCode)
            {
                ObjContactList = JsonConvert.DeserializeObject<JsonCartone>(contactsJson);
               // itemCartList = JsonConvert.DeserializeObject<JsonCartone>(contactsJson);
                cartone.ItemsSource = ObjContactList.CartDetails;
                foreach (JsonCart t in ObjContactList.CartDetails)
                {
                    newtot = t.Total + newtot;
                    //FlagNu = t.FlagNum;
                }

            }
            total.Text =Convert.ToString(newtot);
            gandtot.Text = Convert.ToString(newtot + tax);
            GC.Collect(1);
            // super.finalize();

        }

        public void table_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MasterDetail());
                      
        }

        public void Table_btn_Clicked(object sender, EventArgs e)
        {
            int newnumber = 0;
            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            TicketNumber tota = new TicketNumber();
            r.Table<TicketNumber>();
            r.DeleteAll<TicketNumber>();

            Navigation.PushAsync(new JsonTable(newnumber));
        }

        public void MainbtnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Dashboard());

            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            TempTbl tbr = new TempTbl();
            //SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            s.Table<TempTbl>();
            s.DeleteAll<TempTbl>();

            // tablenow.Text = countremove;
        }

        public void MainMenu_btn_Clicked(object sender, EventArgs e)
        {
            GC.Collect();

            if (Navigation.NavigationStack.Count == 0 ||
                    Navigation.NavigationStack.Last().GetType() != typeof(OnlineTabbed))
            {
                Navigation.PushAsync(new OnlineTabbed(), true);
            }
            else
            {
                Navigation.PushAsync(Navigation.NavigationStack.Last());

            }


            GC.Collect();

            // tablenow.Text = countremove;
        }

        private void OnSelectedItem(object sender, SelectedItemChangedEventArgs e)
        {
            // var itemSelectedData = e.SelectedItem as JsonCartone;
            // DisplayAlert("Info", itemSelectedData.Name, "Ok");

            //if ((e.SelectedItem as Contactone) == null)
            //{
            //    var itemSelectedData = e.SelectedItem as Contactone;
            //    Contactone dd = new Contactone();
            //    dd.ID = (e.SelectedItem as Contactone).ID;
            //    dd.Name = (e.SelectedItem as Contactone).Name;
            //    dd.Price = (e.SelectedItem as Contactone).Price;
            //    var itemSelectedDataq = cartone;
            //    var sss = itemCartList;
            //}

            //(select * from cartone where id = e.SelectedItem) as Contactone;

            //  Contactone = (from i in itemCartList select i).ToList();

            //var Itemdata = (sender as Label).BindingContext as JsonCart;
            //DisplayAlert("Info", itemSelectedData.Name, "Ok");

            //async void DeleteItem()
            //{
            //    var client = new HttpClient();
            //    var uri = new Uri(string.Format("http://192.168.43.226/DeleteProduct.php"+itemSelectedData.ID));
            //    var response = await client.DeleteAsync(uri);
            //    if (response.IsSuccessStatusCode)
            //    {

            //    }
            //}


        }

        public void KOT_btn_Clicked(object sender, EventArgs e)
        {
            //TableQuery p = new TableQuery();
            //SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //s.Table<TempTbl>();
            //var count = s.ExecuteScalar<string>("SELECT max(TblName) FROM TempTbl");

            TicketQuery y = new TicketQuery();
            SQLiteConnection d;
            d = DependencyService.Get<ISQLite>().GetConnection();
            d.Table<TicketNumber>();
            var tikcount = d.ExecuteScalar<string>("SELECT TicketNum FROM TicketNumber");
            //ticketnow.Text = tikcount;

            //string flag = "1";
            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("TestTicket", tikcount.Replace("\r\n", "")));
            postData.Add(new KeyValuePair<string, string>("IsReadyToKot", Convert.ToString(1)));
            var content = new FormUrlEncodedContent(postData);
           // var response = client.PostAsync("http://192.168.43.226/ConfirmKOTready.php", content);
            var response = client.PostAsync(Constants.BaseUrlpos + "ConfirmKOTready.php", content);

            DisplayAlert("YOUR ORDER CONFIRMED AND SENT TO KOT", "thank you", "OK");

            Navigation.PushAsync(new KOTsendPage());

        }

        public void Cart_btn_Clicked(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new Cart());
            //cartone.ItemsSource = update_Item;
            //SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //TempTbl tbr = new TempTbl();
            ////SQLiteConnection s;
            //s = DependencyService.Get<ISQLite>().GetConnection();
            //s.Table<TempTbl>();
            //s.DeleteAll<TempTbl>();

            SQLiteConnection r;
            r = DependencyService.Get<ISQLite>().GetConnection();
            TotalTbl tota = new TotalTbl();
            r.Table<TotalTbl>();
            r.DeleteAll<TotalTbl>();

        }

        public void OnMore(object sender, EventArgs e)
        {
            //var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", "EDIT" + " more context action", "OK");


        }

        public void OnDelete(object sender, EventArgs e)
        {
            //ObservableCollection<JsonCartone> update_Item = new ObservableCollection<JsonCartone>();

            TotalQuery c = new TotalQuery();
            SQLiteConnection s;
            s = DependencyService.Get<ISQLite>().GetConnection();
            s.Table<TotalTbl>();

            var mi = ((MenuItem)sender);
            //await DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
            // cartone.ItemsSource = YourList;
            // cartone.BeginRefresh();
            var clienttwo = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("ItemCode", Convert.ToString(mi.CommandParameter)));
            var content = new FormUrlEncodedContent(postData);
            //var responseon = clienttwo.PostAsync("http://192.168.43.226/DeleteItem.php", content);
            var responseon = clienttwo.PostAsync(Constants.BaseUrlpos + "DeleteItem.php", content);
            s.Delete<TotalTbl>(mi.CommandParameter);
           // cartone.ItemsSource = update_Item;
           // cartone.ItemsSource = c.GetTablelist();
            total.Text = c.GetTotal() + "";

            GetJSON();
            //  string contactsJso = responseon.Content.ReadAsStringAsync().Result;
            //  response.Content.ReadAsStringAsync().Result;
            //JsonCartone ObjContactList = new JsonCartone();
            //if (responseon.IsCompleted)
            //{
            //    //     ObjContactList = JsonConvert.DeserializeObject<JsonCartone>(contactsJso);
            //    cartone.ItemsSource = ObjContactList.CartDetails;
            //}

            //Convert.ToString(mi.CommandParameter)
        }

        //public async void Getdlete()
        //{
        //    var clienttwo = new HttpClient();
        //    var postData = new List<KeyValuePair<string, string>>();
        //    postData.Add(new KeyValuePair<string, string>("ItemCode", Convert.ToString(1)));
        //    var content = new FormUrlEncodedContent(postData);
        //    var responseon = clienttwo.PostAsync("http://192.168.43.226/DeleteSelect.php", content);            
        //  //  string contactsJso = responseon.Content.ReadAsStringAsync().Result;
        //    //  response.Content.ReadAsStringAsync().Result;
        //    JsonCartone ObjContactList = new JsonCartone();
        //    if (responseon.IsCompleted)
        //    {
        //   //     ObjContactList = JsonConvert.DeserializeObject<JsonCartone>(contactsJso);
        //        cartone.ItemsSource = ObjContactList.CartDetails;
        //    }

        //}




        }
}
