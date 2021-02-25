using MSP_Lab.Services;
using MSP_Lab.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Threading.Tasks;

namespace MSP_Lab
{
    public partial class App : Application
    {
        public static AppDataStore Db { get; private set; }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            Db = await AppDataStore.Create();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
