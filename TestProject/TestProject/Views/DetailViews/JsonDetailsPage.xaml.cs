using Newtonsoft.Json;
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
    public partial class JsonDetailsPage : ContentPage
    {
        string pimage;
        decimal pprice;
        public int thiTable;

        public JsonDetailsPage(int id, byte[]image, String name, string code, String description, decimal price, bool isservicecharge, decimal costprice)
        {
            InitializeComponent();
            this.BackgroundImage = "background.png";
            this.Title = "Product Details";
           // CachedImageRenderer.Init();
            thiTable = JsonTable.tablenew;
            // picker.TextColor = Color.White;
            // GridDetails.BindingContext = SelectedContact;
            ItemID.Text = Convert.ToString(id);
            Name.Text = name;
            codedetails.Text = code;
            Description.Text = description;
            Price.Text = Convert.ToString(price);
            isservicechargeinfo.Text= Convert.ToString(isservicecharge);
            CostPriceDetail.Text = Convert.ToString(costprice);
            // Image.Text = image;
            Image.Source = ImageSource.FromStream(() => new MemoryStream(image));

            // Image.Source =ImageService.Instance.LoadCompiledResource(nameOfResource).Into(_imageView);
            // pimage = image;
            //var pimage = ImageSource.FromStream(() => new MemoryStream(image));
            pprice = price;

            //long freeMemory = Java.Lang.Runtime.GetRuntime().FreeMemory();
            //DisplayAlert("freemomory", Convert.ToString(freeMemory), "OK");


            //Widget widget = new Widget()
            //widget.Name = "test"
            //widget.Price = 1;
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:44268/api/test");
            //client.SendAsync(new HttpRequestMessage<Widget>(widget)).ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());

            GetSimilerJSON();
            GC.Collect(1);

            //mGridGallery.ItemClick -= OnItemClicked;
            //mGridGallery.ItemClick += OnItemClicked;


        }
        protected override void OnDisappearing()
        {
            // ImagesStackLayout.Children.Clear();
            //list
            Image.Source = null;
            GC.Collect();
           // this.Content = null;
            this.BindingContext = null;
        }


        public async void GetSimilerJSON()
        {
            var itemsr = string.Empty;
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(Constants.BaseUrlpos + "GetSimmiler.php");
            ContectList ObjItemList = new ContectList();
            if (response.IsSuccessStatusCode)
            {
                string NoteInfoList = response.Content.ReadAsStringAsync().Result;
                ObjItemList = JsonConvert.DeserializeObject<ContectList>(NoteInfoList);
                // listviewNotes.ItemsSource = ObjItemList.NoteInfo;
                foreach (Contactone t in ObjItemList.contacts)
                {

                    // FlagNu = t.Name;
                   // List<Contactone> itemsr = new List<Contactone>(ObjItemList.contacts);
                    itemsr = t.Name;
                   // titlesimi.Text = t.Name;
                }
                //var FlagNuf = new List<JsonItemnoteClass>();

            }
            List<Contactone> similerDeatils = new List<Contactone>(ObjItemList.contacts);

            //var names = new List<String>
            //{
            //    "Sam","Pan","Jam"
            //};

            //var images = new List<String>
            //{
            //    "https://www.gourmetguide.co.uk/shop/wp-content/uploads/2016/06/Resturant-Food-400x200.jpg",
            //    "http://beranisehat.com/wp-content/uploads/2015/06/Healthy-Breakfast-Food-Ideas-400x200.jpg",
            //    "http://empireoftheincasrestaurant.com/wp-content/uploads/2016/05/Papa-a-la-huaicaina-3-400x200.jpg"
            //};

            // MainCarouselView.ItemsSource = similerDeatils; // images; //names;

            //FileImageSource[] imageSources = new[] 
            //{
            //     //FileImageSource.FromFile("GSP1.png"),
            //     //    FileImageSource.FromFile("GSP2.png")
            // };
            GC.Collect(1);

            ObservableCollection<Contactone> imageCollection = new ObservableCollection<Contactone>(ObjItemList.contacts);

            MainCarouselView.ItemsSource = similerDeatils;
            //Device.StartTimer(TimeSpan.FromSeconds(3), (Func<bool>)(() =>
            //{
            //    MainCarouselView.Position = (MainCarouselView.Position + 1) % similerDeatils.Count;

            //    return true;
            //}));

            GC.Collect(1);
        }

        //SelectMultipleBasePage<CheckItem> multiPage;
        SelectMultipleBasePage<JsonItemnoteClass> multiPage;

        private void MainCarouselView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           var ti = e.SelectedItem as Contactone;
            // DisplayActionSheet("You have selected", ti.Name, "OK");
            titlesimi.Text = ti.Name;
            
        }


        public void Add_btn_Clicked(object sender, EventArgs e)
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
            var tikcount = d.ExecuteScalar<string>("SELECT max(TicketNum) FROM TicketNumber");
            // DisplayAlert("tick", tikcount, "ok");
            // ticketnow.Text = tikcount;

            //StuwaQuery n = new StuwaQuery();
            //SQLiteConnection k;
            //k = DependencyService.Get<ISQLite>().GetConnection();
            //k.Table<StuwardTbl>();
            //var Stu = k.ExecuteScalar<string>("SELECT StuwaName FROM StuwardTbl");
            //var stid = k.ExecuteScalar<int>("SELECT StuwaID FROM StuwardTbl");

            int itmID = Convert.ToInt16(ItemID.Text);
            decimal qty = Convert.ToDecimal(Qty.Text);
            decimal total = Convert.ToDecimal(Price.Text);
            decimal Total = (qty * total);
            int BatchNo = Convert.ToInt16(Batch.Text); 
            //Total =+Total;
            // tablenow.Text = count;

            var client = new HttpClient();
            var postData = new List<KeyValuePair<string, string>>();
            string name = OnlineLoginPage.UserName;
            int UserIDn = OnlineLoginPage.UserID;
            // string uid = OnlineLoginPage.us
            int fromApp = 1;
            int NoRadyKOT = 0;
            var deviceID = TestProject.Data.Hardware.Default.DeviceId;
            // var device = Resolver.Resolve<IDevice>();

            // var table = s.Table<CartRecord>();
            //  foreach (var item in table)
            {
                postData.Add(new KeyValuePair<string, string>("Language", Convert.ToString(1)));//Language
                postData.Add(new KeyValuePair<string, string>("Company", Convert.ToString(1)));//Company
                postData.Add(new KeyValuePair<string, string>("Branch", Convert.ToString(1)));//Branch
                postData.Add(new KeyValuePair<string, string>("Location", Convert.ToString(1)));//Location
                postData.Add(new KeyValuePair<string, string>("ItemID", ItemID.Text));//ItemID
                postData.Add(new KeyValuePair<string, string>("ItemCode", codedetails.Text));//ItemCode
                postData.Add(new KeyValuePair<string, string>("Name", Name.Text));
                postData.Add(new KeyValuePair<string, string>("CostPrice", CostPriceDetail.Text));//CostPrice
                postData.Add(new KeyValuePair<string, string>("Price", Price.Text));
                postData.Add(new KeyValuePair<string, string>("Quantity", Qty.Text));
                postData.Add(new KeyValuePair<string, string>("Transaction", Convert.ToString(1)));//Transaction
                postData.Add(new KeyValuePair<string, string>("Reciptno", Convert.ToString(1)));//Reciptno
                postData.Add(new KeyValuePair<string, string>("UserID", Convert.ToString(UserIDn)));//UserID
                postData.Add(new KeyValuePair<string, string>("UserName", name));//UserName
                postData.Add(new KeyValuePair<string, string>("Unit", Convert.ToString(1)));//Unit
                postData.Add(new KeyValuePair<string, string>("Terminal", Convert.ToString(1)));//Terminal
                postData.Add(new KeyValuePair<string, string>("IsServiceChargeOnItem", isservicechargeinfo.Text));///////
                postData.Add(new KeyValuePair<string, string>("SaleTypeId", Convert.ToString(1)));
                postData.Add(new KeyValuePair<string, string>("UnitOfMeasureId", Convert.ToString(1)));//// not
                postData.Add(new KeyValuePair<string, string>("BatchNumber", Convert.ToString(BatchNo)));//BatchNumber
                postData.Add(new KeyValuePair<string, string>("ExpiryDate", Convert.ToString(null)));
                postData.Add(new KeyValuePair<string, string>("TaxOne", Convert.ToString(0)));//TaxOne
                postData.Add(new KeyValuePair<string, string>("TaxTwo", Convert.ToString(0)));//TaxTwo
                postData.Add(new KeyValuePair<string, string>("TaxThree", Convert.ToString(0)));//TaxThree
                postData.Add(new KeyValuePair<string, string>("TaxFour", Convert.ToString(0)));//TaxFour
                postData.Add(new KeyValuePair<string, string>("TaxFive", Convert.ToString(0)));//TaxFive
                postData.Add(new KeyValuePair<string, string>("TaxpersentOne", Convert.ToString(0.00)));//TaxpersentOne
                postData.Add(new KeyValuePair<string, string>("TaxpersentTwo", Convert.ToString(0.00)));//TaxpersentTwo
                postData.Add(new KeyValuePair<string, string>("TaxpersentThree", Convert.ToString(0.00)));//TaxpersentThree
                postData.Add(new KeyValuePair<string, string>("TaxpersentFour", Convert.ToString(0)));//TaxpersentFour
                postData.Add(new KeyValuePair<string, string>("TaxpersentFive", Convert.ToString(0)));//TaxpersentFive
//              postData.Add(new KeyValuePair<string, string>("CustomerID", Convert.ToString(0)));//CustomerID bigint
//              postData.Add(new KeyValuePair<string, string>("Customer", Convert.ToString(0)));//CustomerName varchar
//              postData.Add(new KeyValuePair<string, string>("CustomerType", Convert.ToString(0)));//CustomerTyp int
//              postData.Add(new KeyValuePair<string, string>("IsPromotion", Convert.ToString(0)));//Ispromotion bit
                postData.Add(new KeyValuePair<string, string>("FixedDiscount", Convert.ToString(0.00)));/// not
                postData.Add(new KeyValuePair<string, string>("FixedDiscountPercentage", Convert.ToString(0.00)));//// not 
//              postData.Add(new KeyValuePair<string, string>("PromotionID", Convert.ToString(0)));//IspromotionID bigint
                postData.Add(new KeyValuePair<string, string>("OrderCounter", Convert.ToString(3)));//OrderCounter
                postData.Add(new KeyValuePair<string, string>("Ticket", Convert.ToString(thiTable)));
                postData.Add(new KeyValuePair<string, string>("KotBotCounterId", Convert.ToString(0)));
                postData.Add(new KeyValuePair<string, string>("TestTicket", tikcount.Replace("\r\n", "")));
                postData.Add(new KeyValuePair<string, string>("OrderNum", Convert.ToString(3)));//OrderNum
                postData.Add(new KeyValuePair<string, string>("IsNew", Convert.ToString(1)));
                postData.Add(new KeyValuePair<string, string>("StuwardID", Convert.ToString(StuwardSelect.StuwardIDn)));//StuwardID
                postData.Add(new KeyValuePair<string, string>("StuwardName", StuwardSelect.StuwardNme));//StuwardName
                postData.Add(new KeyValuePair<string, string>("CurrentRowNo", Convert.ToString(0))); // not  
                postData.Add(new KeyValuePair<string, string>("IsAddonItem", Convert.ToString(0)));
                postData.Add(new KeyValuePair<string, string>("IsRetailItem", Convert.ToString(0)));///////////////
                postData.Add(new KeyValuePair<string, string>("BasedAddOnItemId", Convert.ToString(0)));
//              postData.Add(new KeyValuePair<string, string>("PPno", Convert.ToString(0)));//PPnumber varchar(30)
//              postData.Add(new KeyValuePair<string, string>("BPno", Convert.ToString(0)));//BPnumber varchar(30)
                postData.Add(new KeyValuePair<string, string>("IsReadyToKot", Convert.ToString(NoRadyKOT)));
                postData.Add(new KeyValuePair<string, string>("IsFromApp", Convert.ToString(fromApp)));//
                          

                var content = new FormUrlEncodedContent(postData);
               // var response = client.PostAsync("http://192.168.43.226/cartorderInsertNew.php", content);// correct one 1 
                // var response = client.PostAsync("http://192.168.43.226/CartOrderInsert.php", content);
               // var response = client.PostAsync("http://192.168.43.226/InsertSPNewCart.php", content); // test for SP in php
              // var response = client.PostAsync("http://192.168.43.226/cartorderInsertNewSP.php", content);  // correct two sp test//////////////////
                 var response = client.PostAsync(Constants.BaseUrlpos + "cartorderInsertNewSP.php", content);  // correct two sp test//////////////////
               // http://192.168.43.226/cartorderInsertNewSP.php

            }


            Image.Source = null;

            ///////----- end of modification --------------------------
            Navigation.PushAsync(new OnlineCart(Total));

            GC.Collect(1);
            // await _navigation.PushAsync(new Page2(argument_goes_here));


        }

        //public class ByteArrayToImageSourceConverter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //    {
        //        ImageSource retSource = null;
        //        if (value != null)
        //        {
        //            byte[] imageAsBytes = (byte[])value;
        //            byte[] decodedByteArray = System.Convert.FromBase64String(Encoding.UTF8.GetString(imageAsBytes, 0, imageAsBytes.Length));
        //            var stream = new MemoryStream(decodedByteArray);
        //            retSource = ImageSource.FromStream(() => stream);
        //        }
        //        return retSource;
        //    }
        //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var picker = (Picker)sender;
        //    int selectedIndex = picker.SelectedIndex;

        //    if (selectedIndex != -1)
        //    {
        //        PickerLabelthree.Text = picker.Items[selectedIndex];
        //    }

        //    //if (selectedIndex == 0)
        //    //{
        //    //    extwo.Text = "Small Poration";
        //    //}

        //    //if (selectedIndex == 1)
        //    //{
        //    //    extwo.Text = "Reguler Poration";
        //    //}

        //    //if (selectedIndex == 2)
        //    //{
        //    //    extwo.Text = "Large Poration";
        //    //}
        //}

        async void OnClick(object sender, EventArgs ea)
        {
            var itemsr = string.Empty;
            var client = new System.Net.Http.HttpClient();
            var response = await client.GetAsync(Constants.BaseUrlpos + "GetItemNote.php");
            JsonItemNote ObjItemList = new JsonItemNote();
            if (response.IsSuccessStatusCode)
            {
                string NoteInfoList = response.Content.ReadAsStringAsync().Result;
                ObjItemList = JsonConvert.DeserializeObject<JsonItemNote>(NoteInfoList);
               // listviewNotes.ItemsSource = ObjItemList.NoteInfo;
                foreach (JsonItemnoteClass t in ObjItemList.NoteInfo)
                {

                    // FlagNu = t.Name;
                    itemsr =t.Name; 
                    
                }
               //var FlagNuf = new List<JsonItemnoteClass>();

            }
            List<JsonItemnoteClass> items = new List<JsonItemnoteClass>(ObjItemList.NoteInfo);
            if (multiPage == null)
                multiPage = new SelectMultipleBasePage<JsonItemnoteClass>(items) { Title = "CHECK EXTRA ADD-ONS" };

            await Navigation.PushAsync(multiPage);
            //await Navigation.PushAsync(multiPage);
            // var items = new List<JsonItemnoteClass>();
            //items.Add(new CheckItem { Name = "NO PEPPER" });
            //items.Add(new CheckItem { Name = "SPICY" });
            //items.Add(new CheckItem { Name = "DEVELLED" });
            //items.Add(new CheckItem { Name = "FRY" });
            //items.Add(new CheckItem { Name = "LESS CHILLY" });
            //items.Add(new CheckItem { Name = "LESS SPICY" });
            //items.Add(new CheckItem { Name = "HOT" });            
            // multiPage.BackgroundColor = Color.Orange;
            //if (multiPage == null)
            //    multiPage = new SelectMultipleBasePage<JsonItemnoteClass>(items) { Title = "CHECK EXTRA ADD-ONS" };

            //await Navigation.PushAsync(multiPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (multiPage != null)
            {
                results.Text = "";
                var answers = multiPage.GetSelection();
                foreach (var a in answers)
                {
                    results.Text += a.Name + ", ";
                }
            }
            else
            {
                results.Text = "NO EXTRA ADD-ONS";
            }
        }
        
    }
}
