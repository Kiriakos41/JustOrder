using JustOrder.Models;
using SQLite;
using Xamarin.Forms;
using JustOrder.Views;

namespace JustOrder
{
    public partial class App : Application
    {
        public static SQLiteAsyncConnection DbPath;
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTc1Mzc4QDMxMzkyZTM0MmUzMG5NamV5YnAzV2xWZXJqazFSblIyL0tlY3laTFlFV25hc3lPRUtTb2pWekk9");
            MainPage = new AppShell();
        }
        public App(SQLiteAsyncConnection dbPath) : this()
        {
            DbPath = dbPath;
        }

        protected override void OnStart()
        {
            DbPath.CreateTableAsync<Burger>();
            DbPath.CreateTableAsync<Cart>();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
