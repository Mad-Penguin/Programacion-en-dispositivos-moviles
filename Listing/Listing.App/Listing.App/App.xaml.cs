using Listing.App.Services;
using Listing.App.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Listing.App
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginRegisterPage());
        }

        protected override async void OnStart()
        {
            await Shell.Current.GoToAsync("//LoginRegisterPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
